using Concesionaria.Admin.DTOs;

namespace Concesionaria.Admin.Services.Interfaces
{
    public interface IConsultasContactoService
    {
        Task<IEnumerable<ConsultaContactoDto>?> GetConsultasContactoAsync(string token);
        Task<ConsultaContactoDto?> GetConsultaContactoByIdAsync(int id, string token);
        Task<ConsultaContactoDto> CrearConsultaContactoAsync(ConsultaContactoCreacionDto consulta, string token);
        Task<bool> MarcarConsultaContactoLeidaByIdAsync(int id, bool leida, string token);
        Task<bool> EliminarConsultaContactoByIdAsync(int id, string token);
    }
}