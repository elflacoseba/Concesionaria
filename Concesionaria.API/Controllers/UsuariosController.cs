using Concesionaria.API.Data.Entities;
using Concesionaria.API.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
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
        private readonly IEmailSender _emailSender;

        public UsuariosController(
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _configuration = configuration;
            _emailSender = emailSender;
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

            var user = new ApplicationUser { 
                UserName = dto.UserName,
                Email = dto.Email,
                Nombre = dto.Nombre,
                Apellido = dto.Apellido
            };

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

        [HttpPost("GenerarResetPasswordToken")]
        [AllowAnonymous]
        [EndpointSummary("Genera un token para resetear la contraseña y devuelve el link de reseteo. Envía un email al usuario con el link para resetear la contraseña.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GenerarResetPasswordToken([FromBody] string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return NotFound("Usuario no encontrado.");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            // Aquí puedes construir el link para el frontend Razor Pages, por ejemplo:
            var resetLink = $"{_configuration["FrontendUrl"]}/auth-new-pass?email={Uri.EscapeDataString(email)}&token={Uri.EscapeDataString(token)}";
            
            await _emailSender.SendEmailAsync(email, "Restablecer contraseña", $"Haz clic aquí para restablecer tu contraseña: {resetLink}");

            return Ok(new { resetLink, token });
        }

        [HttpPost("ResetearPassword")]
        [AllowAnonymous]
        [EndpointSummary("Resetea la contraseña de un usuario usando el token de reseteo.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ResetearPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);
            if (user == null)
                return NotFound("Usuario no encontrado.");

            var result = await _userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.NewPassword);
            
            if (result.Succeeded)
                return Ok("Contraseña restablecida correctamente.");
            return BadRequest(result.Errors);
        }

        

        /// <summary>
        /// Construye un token JWT para el usuario especificado en las credenciales.
        /// Incluye los claims del usuario y establece la expiración del token.
        /// </summary>
        /// <param name="credenciales">Credenciales del usuario (email y contraseña).</param>
        /// <returns>Un objeto <see cref="RespuestaAutenticacionDto"/> con el token generado y su fecha de expiración.</returns>
        private async Task<RespuestaAutenticacionDto> ConstruirToken(CredencialesUsuarioDto credenciales)
        {
            var user = await _userManager.FindByEmailAsync(credenciales.Email);
            
            var claims = new List<Claim>
            {
                new Claim("UsuarioEmail", credenciales.Email),
                new Claim("UsuarioNombre", user!.Nombre),
                new Claim("UsuarioApellido", user!.Apellido)
            };

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
