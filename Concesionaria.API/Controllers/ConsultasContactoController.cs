using Concesionaria.Application.DTOs;
using Concesionaria.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace Concesionaria.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ConsultasContactoController : ControllerBase
    {
        private readonly IConsultaContactoService _consultaContactoService;

        public ConsultasContactoController(IConsultaContactoService consultaContactoService)
        {
            _consultaContactoService = consultaContactoService;
        }

        [HttpGet]
        [EndpointSummary("Obtiene todas las consultas de contacto.")]
        [ProducesResponseType<IEnumerable<ConsultaContactoSinMensajeDTO>>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IEnumerable<ConsultaContactoSinMensajeDTO>> Get()
        {
            return await _consultaContactoService.GetAllConsultasContactoAsync();
        }

        [HttpGet("GetPaged")]
        [EndpointSummary("Obtiene las consultas de contacto paginadas.")]
        [ProducesResponseType(typeof(PagedResultDTO<ConsultaContactoDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PagedResultDTO<ConsultaContactoDTO>>> GetPaged([FromQuery][Description("Indica el número de página.")] int pageNumber = 1, [FromQuery][Description("Indica la cantidad de registros por página")] int pageSize = 10)
        {
            var result = await _consultaContactoService.GetConsultasContactoPagedAsync(pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("{id}", Name = "ObtenerConsultaContacto")]
        [EndpointSummary("Obtiene una consulta de contacto por su Id.")]
        [EndpointDescription("Proporciona los detalles de una consulta de contacto específica, identificada por su Id. Si no se encuentra la consulta, devuelve un estado 404 Not Found.")]
        [ProducesResponseType<ConsultaContactoDTO>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
        [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
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
        [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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

        [HttpPatch("{id:int}/leida")]
        [EndpointSummary("Marca una consulta de contacto como leída o no leída.")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> MarcarComoLeida([FromRoute][Description("El Id de la consulta de contacto.")] int id, [FromQuery][Description("Indica si la consulta de contacto se marcará como leída o no leída.")] bool leida)
        {
            var consultaContacto = await _consultaContactoService.GetConsultaContactoByIdAsync(id);

            if (consultaContacto == null)
            {
                return NotFound();
            }

            await _consultaContactoService.MarcarConsultaComoLeidaAsync(id, leida);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [EndpointSummary("Elimina una consulta de contacto por su Id.")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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

        [HttpGet("filtrar")]
        [EndpointSummary("Filtra las consultas de contacto por estado de lectura.")]
        [ProducesResponseType<IEnumerable<ConsultaContactoSinMensajeDTO>>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<ConsultaContactoSinMensajeDTO>>> FiltrarPorNoLeida([FromQuery][Description("Indica que la consulta de contacto se filtrará por el estado de no leída.")] bool noLeida)
        {
            var consultas = await _consultaContactoService.GetConsultasPorEstadoNoLeidaAsync(noLeida);
            return Ok(consultas);
        }
    }
}
