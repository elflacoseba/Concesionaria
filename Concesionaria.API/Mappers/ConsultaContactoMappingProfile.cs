using Mapster;
using Concesionaria.API.Models;
using Concesionaria.API.DTOs;

namespace Concesionaria.API.Mappers
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
