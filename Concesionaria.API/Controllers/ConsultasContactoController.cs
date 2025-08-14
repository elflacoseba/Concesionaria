using Concesionaria.Application.DTOs;
using Concesionaria.Application.Exceptions;
using Concesionaria.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

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
        [EndpointSummary("Obtiene todas las consultas de contacto.")]
        public async Task<IEnumerable<ConsultaContactoDTO>> Get()
        {
            return await _consultaContactoService.GetAllConsultasContactoAsync();
        }

        [HttpGet("{id}", Name = "ObtenerConsultaContacto")]
        [EndpointSummary("Obtiene una consulta de contacto por su Id.")]
        [EndpointDescription("Proporciona los detalles de una consulta de contacto específica, identificada por su Id. Si no se encuentra la consulta, devuelve un estado 404 Not Found.")]
        [ProducesResponseType<ConsultaContactoDTO>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]        
        public async Task<ActionResult<ConsultaContactoDTO>> Get([Description("El Id de la consulta de contacto.")] int id)
        {
            var consultaContacto = await _consultaContactoService.GetConsultaContactoByIdAsync(id);

            if (consultaContacto == null)
            {
                return NotFound();
            }

            return consultaContacto;
        }

        [HttpPost]
        [EndpointSummary("Crea una nueva consulta de contacto.")]
        [EndpointDescription("Crea una nueva consulta de contacto con los datos proporcionados. Devuelve un estado 201 Created junto con la ubicación de la nueva consulta.")]
        [ProducesResponseType<ConsultaContactoDTO>(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]        
        public async Task<ActionResult> Post(ConsultaContactoCreacionDTO consultaContactoCreacionDTO)
        {
            if (consultaContactoCreacionDTO == null)
            {
                return BadRequest("El objeto ConsultaContactoCreacionDTO no puede ser nulo.");
            }

            var consultaContacto = await _consultaContactoService.CreateConsultaContactoAsync(consultaContactoCreacionDTO);
            return CreatedAtRoute("ObtenerConsultaContacto", new { id = consultaContacto.Id }, consultaContacto);
        }

        [HttpPut("{id:int}")]
        [EndpointSummary("Actualiza una consulta de contacto existente.")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Put([Description("El Id de la consulta de contacto.")] int id, ConsultaContactoActualizacionDTO consultaContactoActualizacionDTO)
        {
            var consultaContacto = await _consultaContactoService.GetConsultaContactoByIdAsync(id);

            if (consultaContacto == null)
            {
                return NotFound();
            }

            await _consultaContactoService.UpdateConsultaContactoAsync(id, consultaContactoActualizacionDTO);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [EndpointSummary("Elimina una consulta de contacto por su Id.")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Delete([Description("El Id de la consulta de contacto.")] int id)
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
