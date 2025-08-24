using Concesionaria.Admin.Models;

namespace Concesionaria.Admin.Services.Interfaces
{
    public interface IConsultasContactoService
    {
        Task<IEnumerable<ConsultaContacto>?> GetConsultasContactoAsync();
        Task<ConsultaContacto?> GetConsultaContactoByIdAsync(int id);
        Task<bool?> MarcarConsultaContactoLeidaByIdAsync(int id, bool leida);
    }
}