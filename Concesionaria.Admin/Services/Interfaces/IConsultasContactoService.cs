using Concesionaria.Admin.DTOs;

namespace Concesionaria.Admin.Services.Interfaces
{
    public interface IConsultasContactoService
    {
    Task<IEnumerable<ConsultaContactoDto>?> GetConsultasContactoAsync();
    Task<ConsultaContactoDto?> GetConsultaContactoByIdAsync(int id);
    Task<ConsultaContactoDto> CrearConsultaContactoAsync(ConsultaContactoCreacionDto consulta);
        Task<bool> MarcarConsultaContactoLeidaByIdAsync(int id, bool leida);
        Task<bool> EliminarConsultaContactoByIdAsync(int id);
    }
}