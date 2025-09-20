using Concesionaria.API.DTOs;
using Concesionaria.API.Models;
using Concesionaria.API.Data;
using FluentValidation;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace Concesionaria.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultasContactoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IValidator<ConsultaContactoCreacionDTO> _validatorCreacion;
        private readonly IValidator<ConsultaContactoActualizacionDTO> _validatorActualizacion;

        public ConsultasContactoController(ApplicationDbContext context, IValidator<ConsultaContactoCreacionDTO> validatorCreacion, IValidator<ConsultaContactoActualizacionDTO> validatorActualizacion)
        {
            _context = context;
            _validatorCreacion = validatorCreacion;
            _validatorActualizacion = validatorActualizacion;
        }

        [HttpGet]
        [EndpointSummary("Obtiene todas las consultas de contacto.")]
        [ProducesResponseType<IEnumerable<ConsultaContactoSinMensajeDTO>>(StatusCodes.Status200OK)]
        public async Task<IEnumerable<ConsultaContactoSinMensajeDTO>> Get()
        {
            var consultas = await _context.ConsultasContacto
                .Select(c => new { c.Id, c.Nombre, c.Apellido, c.Email, c.Telefono, c.FechaEnvio, c.FechaLectura, c.NoLeida })
                .ToListAsync();
            return consultas.Adapt<IEnumerable<ConsultaContactoSinMensajeDTO>>();
        }

        [HttpGet("GetPaged")]
        [EndpointSummary("Obtiene las consultas de contacto paginadas.")]
        [ProducesResponseType(typeof(PagedResultDTO<ConsultaContactoDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedResultDTO<ConsultaContactoDTO>>> GetPaged([FromQuery][Description("Indica el número de página.")] int pageNumber = 1, [FromQuery][Description("Indica la cantidad de registros por página")] int pageSize = 10)
        {
            var query = _context.ConsultasContacto.AsNoTracking();
            var totalCount = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return Ok(new PagedResultDTO<ConsultaContactoDTO>
            {
                Items = items.Adapt<IEnumerable<ConsultaContactoDTO>>(),
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            });
        }

        [HttpGet("{id}", Name = "ObtenerConsultaContacto")]
        [EndpointSummary("Obtiene una consulta de contacto por su Id.")]
        [EndpointDescription("Proporciona los detalles de una consulta de contacto específica, identificada por su Id. Si no se encuentra la consulta, devuelve un estado 404 Not Found.")]
        [ProducesResponseType<ConsultaContactoDTO>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ConsultaContactoDTO>> Get([Description("El Id de la consulta de contacto.")] int id)
        {
            var consulta = await _context.ConsultasContacto.FindAsync(id);
            if (consulta == null)
                return NotFound();
            return consulta.Adapt<ConsultaContactoDTO>();
        }

        [HttpPost]
        [EndpointSummary("Crea una nueva consulta de contacto.")]
        [EndpointDescription("Crea una nueva consulta de contacto con los datos proporcionados. Devuelve un estado 201 Created junto con la ubicación de la nueva consulta.")]
        [ProducesResponseType<ConsultaContactoDTO>(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Post(ConsultaContactoCreacionDTO consultaContactoCreacionDTO)
        {
            if (consultaContactoCreacionDTO == null)
                return BadRequest("El objeto ConsultaContactoCreacionDTO no puede ser nulo.");

            var validationResult = await _validatorCreacion.ValidateAsync(consultaContactoCreacionDTO);
            if (!validationResult.IsValid)
                return ValidationProblem(new ValidationProblemDetails(validationResult.ToDictionary()));

            var consulta = consultaContactoCreacionDTO.Adapt<ConsultaContacto>();
            _context.ConsultasContacto.Add(consulta);
            await _context.SaveChangesAsync();
            var dto = consulta.Adapt<ConsultaContactoDTO>();
            return CreatedAtRoute("ObtenerConsultaContacto", new { id = dto.Id }, dto);
        }

        [HttpPut("{id:int}")]
        [EndpointSummary("Actualiza una consulta de contacto existente.")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Put([Description("El Id de la consulta de contacto.")] int id, ConsultaContactoActualizacionDTO consultaContactoActualizacionDTO)
        {
            var consulta = await _context.ConsultasContacto.FindAsync(id);
            if (consulta == null)
                return NotFound();

            var validationResult = await _validatorActualizacion.ValidateAsync(consultaContactoActualizacionDTO);
            if (!validationResult.IsValid)
                return ValidationProblem(new ValidationProblemDetails(validationResult.ToDictionary()));

            var fechaEnvio = consulta.FechaEnvio;
            consultaContactoActualizacionDTO.Adapt(consulta);
            consulta.Id = id;
            consulta.FechaEnvio = fechaEnvio;
            _context.ConsultasContacto.Update(consulta);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id:int}/leida")]
        [EndpointSummary("Marca una consulta de contacto como leída o no leída.")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> MarcarComoLeida([FromRoute][Description("El Id de la consulta de contacto.")] int id, [FromQuery][Description("Indica si la consulta de contacto se marcará como leída o no leída.")] bool leida)
        {
            var consulta = await _context.ConsultasContacto.FindAsync(id);
            if (consulta == null)
                return NotFound();
            consulta.NoLeida = !leida;
            consulta.FechaLectura = leida ? DateTime.UtcNow : null;
            _context.ConsultasContacto.Update(consulta);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [EndpointSummary("Elimina una consulta de contacto por su Id.")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Delete([Description("El Id de la consulta de contacto.")] int id)
        {
            var consulta = await _context.ConsultasContacto.FindAsync(id);
            if (consulta == null)
                return NotFound();
            _context.ConsultasContacto.Remove(consulta);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("filtrar")]
        [EndpointSummary("Filtra las consultas de contacto por estado de lectura.")]
        [ProducesResponseType<IEnumerable<ConsultaContactoSinMensajeDTO>>(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ConsultaContactoSinMensajeDTO>>> FiltrarPorNoLeida([FromQuery][Description("Indica que la consulta de contacto se filtrará por el estado de no leída.")] bool noLeida)
        {
            var consultas = await _context.ConsultasContacto
                .Where(c => c.NoLeida == noLeida)
                .Select(c => new { c.Id, c.Nombre, c.Apellido, c.Email, c.Telefono, c.FechaEnvio, c.FechaLectura, c.NoLeida })
                .ToListAsync();
            return Ok(consultas.Adapt<IEnumerable<ConsultaContactoSinMensajeDTO>>());
        }
    }
}
