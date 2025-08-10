using Concesionaria.Application.DTOs;

namespace Concesionaria.Application.Interfaces
{
    public interface IConsultaContactoService
    {
        Task<IEnumerable<ConsultaContactoDTO>> GetAllConsultasContactoAsync();
    }
}