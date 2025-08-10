using AutoMapper;
using Concesionaria.Application.DTOs;
using Concesionaria.Application.Interfaces;
using Concesionaria.Domain.Entities;

namespace Concesionaria.Application.Services
{
    public class ConsultaContactoService :IConsultaContactoService
    {
        private readonly IGenericRepository<ConsultaContacto> _repository;
        private readonly IMapper _mapper;

        public ConsultaContactoService(IGenericRepository<ConsultaContacto> repository, IMapper  mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ConsultaContactoDTO>> GetAllConsultasContactoAsync()
        {
            var consultas = await _repository.GetAllAsync();
            
            return _mapper.Map<IEnumerable<ConsultaContactoDTO>>(consultas);
        }

        public async Task<ConsultaContactoDTO> GetConsultaContactoByIdAsync(int id)
        {
            var consulta = await _repository.GetByIdAsync(id);

            return _mapper.Map<ConsultaContactoDTO>(consulta);
        }

        public async Task<ConsultaContactoDTO> CreateConsultaContactoAsync(ConsultaContactoCreacionDTO consultaContactoCreacionDTO)
        {
            if (consultaContactoCreacionDTO == null)
            {
                throw new ArgumentNullException(nameof(consultaContactoCreacionDTO), "ConsultaContactoCreacionDTO no puede ser nulo.");
            }
            var consultaContacto = _mapper.Map<ConsultaContacto>(consultaContactoCreacionDTO);
            await _repository.AddAsync(consultaContacto);
            await _repository.SaveChangesAsync();

            return await Task.FromResult(_mapper.Map<ConsultaContactoDTO>(consultaContacto));

        }
    }
}
