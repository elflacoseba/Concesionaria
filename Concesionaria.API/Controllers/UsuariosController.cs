using Concesionaria.API.Data.Entities;
using Concesionaria.API.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Concesionaria.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsuariosController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public UsuariosController(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpGet]
        [EndpointSummary("Obtiene todos los usuarios.")]
        [ProducesResponseType<IEnumerable<ApplicationUser>>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetUsuarios()
        {
            var users = _userManager.Users.ToList();
            return Ok(users);
        }

        [HttpGet("GetUsuario/{id}")]
        [EndpointSummary("Obtiene un usuario por su ID.")]
        [ProducesResponseType<ApplicationUser>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetUsuario([Description("Id del usuario")] string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpPost]
        [EndpointSummary("Crea un nuevo usuario.")]
        [ProducesResponseType<IdentityUser>(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CrearUsuario([FromBody] UsuarioCrearDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new ApplicationUser { UserName = dto.UserName, Email = dto.Email };
            var result = await _userManager.CreateAsync(user, dto.Password);
            if (result.Succeeded)
                return CreatedAtAction(nameof(GetUsuarios), new { id = user.Id }, user);
            return BadRequest(result.Errors);
        }

        [HttpDelete("{id}")]
        [EndpointSummary("Elimina un usuario por su ID.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> EliminarUsuario([Description("Id del usuario")] string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
                return NoContent();
            return BadRequest(result.Errors);
        }

        [HttpPost("{id}/AsignarRoles")]
        [EndpointSummary("Asigna roles a un usuario.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> AsignarRoles([Description("Id del usuario")] string id, [FromBody] List<string> roles)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();
            var result = await _userManager.AddToRolesAsync(user, roles);
            if (result.Succeeded)
                return Ok();
            return BadRequest(result.Errors);
        }

        [HttpPost("{id}/QuitarRoles")]
        [EndpointSummary("Quita roles a un usuario.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> RemoverRoles([Description("Id del usuario")] string id, [FromBody] List<string> roles)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();
            var result = await _userManager.RemoveFromRolesAsync(user, roles);
            if (result.Succeeded)
                return Ok();
            return BadRequest(result.Errors);
        }

        [HttpGet("{id}/roles")]
        [EndpointSummary("Obtiene los roles de un usuario.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> ObtenerRoles([Description("Id del usuario")] string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();
            var roles = await _userManager.GetRolesAsync(user);
            return Ok(roles);
        }

        [HttpPost("Login")]
        [EndpointDescription("Autentica a un usuario y genera un token JWT.")]
        [ProducesResponseType(typeof(RespuestaAutenticacionDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        public async Task<ActionResult<RespuestaAutenticacionDto>> Login(CredencialesUsuarioDto credenciales)
        {
            var user = await _userManager.FindByEmailAsync(credenciales.Email);
            
            if (user != null && await _userManager.CheckPasswordAsync(user, credenciales.Password))
            {
                return await ConstruirToken(credenciales);
            }

            ModelState.AddModelError(string.Empty, "Credenciales incorrectas.");
            return ValidationProblem();
        }

        /// <summary>
        /// Construye un token JWT para el usuario especificado en las credenciales.
        /// Incluye los claims del usuario y establece la expiración del token.
        /// </summary>
        /// <param name="credenciales">Credenciales del usuario (email y contraseña).</param>
        /// <returns>Un objeto <see cref="RespuestaAutenticacionDto"/> con el token generado y su fecha de expiración.</returns>
        private async Task<RespuestaAutenticacionDto> ConstruirToken(CredencialesUsuarioDto credenciales)
        {
            var claims = new List<Claim>
            {
                new Claim("email", credenciales.Email)
            };

            var user = await _userManager.FindByEmailAsync(credenciales.Email);
            var claimsDB = await _userManager.GetClaimsAsync(user!);

            claims.AddRange(claimsDB);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddHours(8);

            var tokenSeguridad = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: expiration,
                signingCredentials: creds);

            var token = new JwtSecurityTokenHandler().WriteToken(tokenSeguridad);

            return new RespuestaAutenticacionDto
            {
                Token = token,
                Expiracion = expiration
            };

        }
    }
}
