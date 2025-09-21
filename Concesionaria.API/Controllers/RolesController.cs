using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Concesionaria.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpGet]
        [EndpointSummary("Obtiene todos los roles disponibles.")]
        [ProducesResponseType(typeof(IEnumerable<IdentityRole>), StatusCodes.Status200OK)]
        public IActionResult GetRoles()
        {
            var roles = _roleManager.Roles.ToList();
            return Ok(roles);
        }

        [HttpPost]
        [EndpointSummary("Crea un nuevo rol.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateRole([FromBody] string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
                return BadRequest("El nombre del rol es requerido.");

            var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
            if (result.Succeeded)
                return Ok();
            return BadRequest(result.Errors);
        }

        [HttpDelete("{roleName}")]
        [EndpointSummary("Elimina un rol existente.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteRole(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
                return NotFound();
            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
                return NoContent();
            return BadRequest(result.Errors);
        }
    }
}
