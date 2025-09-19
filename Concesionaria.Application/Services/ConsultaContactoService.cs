using Concesionaria.Application.DTOs;
using Concesionaria.Application.Interfaces;
using Concesionaria.Domain.Entities;
using FluentValidation;
using Mapster;

namespace Concesionaria.Application.Services
{
    public class ConsultaContactoService : IConsultaContactoService
    {
        private readonly IGenericRepository<ConsultaContacto> _repository;
        private readonly IValidator<ConsultaContactoCreacionDTO> _validatorConsultaCreacionDTO;
        private readonly IValidator<ConsultaContactoActualizacionDTO> _validatorConsultaActualizacionDTO;

        public ConsultaContactoService(IGenericRepository<ConsultaContacto> repository, IValidator<ConsultaContactoCreacionDTO> validatorConsultaCreacionDTO, IValidator<ConsultaContactoActualizacionDTO> validatorConsultaActualizacionDTO)
        {
            _repository = repository;
            _validatorConsultaCreacionDTO = validatorConsultaCreacionDTO;
            _validatorConsultaActualizacionDTO = validatorConsultaActualizacionDTO;
        }

        public async Task<IEnumerable<ConsultaContactoSinMensajeDTO>> GetAllConsultasContactoAsync()
        {
            var consultas = await _repository.GetSelectedAsync(c => new { c.Id, c.Nombre, c.Apellido, c.Email, c.Telefono, c.FechaEnvio, c.FechaLectura, c.NoLeida });
            return consultas.Adapt<IEnumerable<ConsultaContactoSinMensajeDTO>>();
        }

        public async Task<PagedResultDTO<ConsultaContactoDTO>> GetConsultasContactoPagedAsync(int pageNumber, int pageSize)
        {
            var (items, totalCount) = await _repository.GetPagedAsync(pageNumber, pageSize);
            return new PagedResultDTO<ConsultaContactoDTO>
            {
                Items = items.Adapt<IEnumerable<ConsultaContactoDTO>>(),
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<ConsultaContactoDTO> GetConsultaContactoByIdAsync(int id)
        {
            var consulta = await _repository.GetByIdAsync(id);
            return consulta.Adapt<ConsultaContactoDTO>();
        }

        public async Task<ConsultaContactoDTO> CreateConsultaContactoAsync(ConsultaContactoCreacionDTO consultaContactoCreacionDTO)
        {
            if (consultaContactoCreacionDTO == null)
            {
                throw new ArgumentNullException(nameof(consultaContactoCreacionDTO), "ConsultaContactoCreacionDTO no puede ser nulo.");
            }

            var validationResult = await _validatorConsultaCreacionDTO.ValidateAsync(consultaContactoCreacionDTO);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var consultaContacto = consultaContactoCreacionDTO.Adapt<ConsultaContacto>();
            await _repository.AddAsync(consultaContacto);
            await _repository.SaveChangesAsync();

            return consultaContacto.Adapt<ConsultaContactoDTO>();
        }

        public async Task<int> UpdateConsultaContactoAsync(int id, ConsultaContactoActualizacionDTO consultaContactoActualizacionDTO)
        {
            if (consultaContactoActualizacionDTO == null)
            {
                throw new ArgumentNullException(nameof(consultaContactoActualizacionDTO), "ConsultaContactoActualizacionDTO no puede ser nulo.");
            }

            var consultaContacto = await _repository.GetByIdAsync(id);

            if (consultaContacto == null)
            {
                throw new KeyNotFoundException($"ConsultaContacto con ID {id} no encontrada.");
            }

            var validationResult = await _validatorConsultaActualizacionDTO.ValidateAsync(consultaContactoActualizacionDTO);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var fechaEnvio = consultaContacto.FechaEnvio; // Guardamos la fecha original

            consultaContacto = consultaContactoActualizacionDTO.Adapt<ConsultaContacto>();
            consultaContacto.Id = id; // Aseguramos que el ID se mantenga
            consultaContacto.FechaEnvio = fechaEnvio; // Restauramos la fecha original

            _repository.Update(consultaContacto);
            return await _repository.SaveChangesAsync();
        }

        public async Task<int> MarcarConsultaComoLeidaAsync(int id, bool leida)
        {
            var consulta = await _repository.GetByIdAsync(id);

            if (consulta == null)
                throw new KeyNotFoundException($"ConsultaContacto con ID {id} no encontrada.");

            consulta.NoLeida = !leida;
            consulta.FechaLectura = leida ? DateTime.UtcNow : null;

            _repository.Update(consulta);
            return await _repository.SaveChangesAsync();
        }

        public async Task<int> DeleteConsultaContactoAsync(int id)
        {
            var consulta = await _repository.GetByIdAsync(id);
            if (consulta == null)
            {
                throw new KeyNotFoundException($"ConsultaContacto con ID {id} no encontrada.");
            }
            _repository.Delete(consulta);
            return await _repository.SaveChangesAsync();
        }        

        public async Task<IEnumerable<ConsultaContactoDTO>> GetConsultasPorEstadoNoLeidaAsync(bool noLeida)
        {
            var consultas = await _repository.GetByPredicateAsync(c => c.NoLeida == noLeida);
            return consultas.Adapt<IEnumerable<ConsultaContactoDTO>>();
        }
    }
}
