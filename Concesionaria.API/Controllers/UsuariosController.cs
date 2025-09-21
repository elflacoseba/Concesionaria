using Concesionaria.API.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace Concesionaria.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UsuariosController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
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
        public async Task<IActionResult> ObtenerRoles([Description("Id del usuario")] string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();
            var roles = await _userManager.GetRolesAsync(user);
            return Ok(roles);
        }
    }


}
