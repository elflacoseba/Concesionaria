using AutoMapper;
using Concesionaria.Application.DTOs;
using Concesionaria.Domain.Entities;

namespace Concesionaria.Application.Mappers
{
    public class ConsultaContactoMappingProfile : Profile
    {
        public ConsultaContactoMappingProfile()
        {
            CreateMap<ConsultaContacto, ConsultaContactoDTO>();
        }
    }
}
