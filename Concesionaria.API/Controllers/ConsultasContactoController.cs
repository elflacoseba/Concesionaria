using Concesionaria.Application.DTOs;
using Concesionaria.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Concesionaria.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultasContactoController : ControllerBase
    {
        private readonly IConsultaContactoService _consultaContactoService;

        public ConsultasContactoController(IConsultaContactoService consultaContactoService)
        {
            _consultaContactoService = consultaContactoService;
        }

        [HttpGet]
        public async Task<IEnumerable<ConsultaContactoDTO>> Get()
        {
            return await _consultaContactoService.GetAllConsultasContactoAsync();
        }

        [HttpGet("{id}", Name = "ObtenerConsultaContacto")]
        public async Task<ActionResult<ConsultaContactoDTO>> Get(int id)
        {
            var consultaContacto = await _consultaContactoService.GetConsultaContactoByIdAsync(id);

            if (consultaContacto == null)
            {
                return NotFound();
            }

            return consultaContacto;
        }

        [HttpPost]
        public async Task<ActionResult> Post(ConsultaContactoCreacionDTO consultaContactoCreacionDTO)
        {
            if (consultaContactoCreacionDTO == null)
            {
                return BadRequest("El objeto ConsultaContactoCreacionDTO no puede ser nulo.");
            }

            var consultaContacto = await _consultaContactoService.CreateConsultaContactoAsync(consultaContactoCreacionDTO);
            return CreatedAtRoute("ObtenerConsultaContacto", new { id = consultaContacto.Id }, consultaContacto);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var consultaContacto = await _consultaContactoService.GetConsultaContactoByIdAsync(id);

            if (consultaContacto == null)
            {
                return NotFound();
            }

            var result = await _consultaContactoService.DeleteConsultaContactoAsync(id);

            return NoContent();
        }
    }
}
