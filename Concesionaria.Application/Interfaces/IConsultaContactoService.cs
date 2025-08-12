using Concesionaria.Application.DTOs;

namespace Concesionaria.Application.Interfaces
{
    public interface IConsultaContactoService
    {
        Task<IEnumerable<ConsultaContactoDTO>> GetAllConsultasContactoAsync();
        Task<ConsultaContactoDTO> GetConsultaContactoByIdAsync(int id);
        Task<ConsultaContactoDTO> CreateConsultaContactoAsync(ConsultaContactoCreacionDTO consultaContactoCreacionDTO);
        Task<int> UpdateConsultaContactoAsync(int id, ConsultaContactoActualizacionDTO consultaContactoActualizacionDTO);
        Task<int> DeleteConsultaContactoAsync(int id);
    }
}