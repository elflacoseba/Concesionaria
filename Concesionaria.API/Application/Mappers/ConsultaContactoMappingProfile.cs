using Mapster;
using Concesionaria.API.Application.DTOs;
using Concesionaria.API.Domain.Entities;

namespace Concesionaria.API.Application.Mappers
{
    public static class ConsultaContactoMappingConfig
    {
        public static void RegisterMappings()
        {
            TypeAdapterConfig<ConsultaContacto, ConsultaContactoDTO>.NewConfig();
            TypeAdapterConfig<ConsultaContactoCreacionDTO, ConsultaContacto>.NewConfig();
            TypeAdapterConfig<ConsultaContactoActualizacionDTO, ConsultaContacto>.NewConfig();
        }
    }
}
