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

    }
}
