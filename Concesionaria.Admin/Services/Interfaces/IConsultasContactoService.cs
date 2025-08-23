using Concesionaria.Admin.Models;

namespace Concesionaria.Admin.Services.Interfaces
{
    public interface IConsultasContactoService
    {
        Task<IEnumerable<ConsultaContacto>?> GetConsultasContactoAsync();
    }
}