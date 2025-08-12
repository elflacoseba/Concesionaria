using Concesionaria.Application.DTOs;
using Concesionaria.Application.Interfaces;
using Concesionaria.Domain.Entities;
using Mapster;

namespace Concesionaria.Application.Services
{
    public class ConsultaContactoService : IConsultaContactoService
    {
        private readonly IGenericRepository<ConsultaContacto> _repository;

        public ConsultaContactoService(IGenericRepository<ConsultaContacto> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ConsultaContactoDTO>> GetAllConsultasContactoAsync()
        {
            var consultas = await _repository.GetAllAsync();
            return consultas.Adapt<IEnumerable<ConsultaContactoDTO>>();
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
            var consultaContacto = consultaContactoCreacionDTO.Adapt<ConsultaContacto>();
            await _repository.AddAsync(consultaContacto);
            await _repository.SaveChangesAsync();

            return consultaContacto.Adapt<ConsultaContactoDTO>();
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
    }
}
