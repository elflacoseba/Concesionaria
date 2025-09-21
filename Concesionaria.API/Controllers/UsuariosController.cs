using Concesionaria.API.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Concesionaria.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsuariosController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        [EndpointSummary("Obtiene todos los usuarios.")]
        [ProducesResponseType<IEnumerable<IdentityUser>>(StatusCodes.Status200OK)]
        public IActionResult GetUsuarios()
        {
            var users = _userManager.Users.ToList();            
            return Ok(users);
        }

        [HttpGet("GetUsuario/{id}")]
        [EndpointSummary("Obtiene un usuario por su ID.")]
        [ProducesResponseType<IdentityUser>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUsuario(string id)
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
        public async Task<IActionResult> CrearUsuario([FromBody] UsuarioCrearDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new IdentityUser { UserName = dto.UserName, Email = dto.Email };
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
        public async Task<IActionResult> EliminarUsuario(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
                return NoContent();
            return BadRequest(result.Errors);
        }

        [HttpPost("{id}/roles")]
        [EndpointSummary("Asigna roles a un usuario.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AsignarRoles(string id, [FromBody] List<string> roles)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();
            var result = await _userManager.AddToRolesAsync(user, roles);
            if (result.Succeeded)
                return Ok();
            return BadRequest(result.Errors);
        }

        [HttpGet("{id}/roles")]
        [EndpointSummary("Obtiene los roles de un usuario.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObtenerRoles(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();
            var roles = await _userManager.GetRolesAsync(user);
            return Ok(roles);
        }
    }

    
}
