using Mapster;
using Concesionaria.Application.DTOs;
using Concesionaria.Domain.Entities;

namespace Concesionaria.Application.Mappers
{
    public static class ConsultaContactoMappingConfig
    {
        public static void RegisterMappings()
        {
            TypeAdapterConfig<ConsultaContacto, ConsultaContactoDTO>.NewConfig();
            TypeAdapterConfig<ConsultaContactoCreacionDTO, ConsultaContacto>.NewConfig();
        }
    }
}
