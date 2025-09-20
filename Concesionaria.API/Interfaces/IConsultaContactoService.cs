using Concesionaria.API.DTOs;

namespace Concesionaria.API.Interfaces
{
    public interface IConsultaContactoService
    {
        Task<IEnumerable<ConsultaContactoSinMensajeDTO>> GetAllConsultasContactoAsync();
        Task<PagedResultDTO<ConsultaContactoDTO>> GetConsultasContactoPagedAsync(int pageNumber, int pageSize);
        Task<ConsultaContactoDTO> GetConsultaContactoByIdAsync(int id);
        Task<ConsultaContactoDTO> CreateConsultaContactoAsync(ConsultaContactoCreacionDTO consultaContactoCreacionDTO);
        Task<int> UpdateConsultaContactoAsync(int id, ConsultaContactoActualizacionDTO consultaContactoActualizacionDTO);
        Task<int> MarcarConsultaComoLeidaAsync(int id, bool leida);
        Task<int> DeleteConsultaContactoAsync(int id);
        Task<IEnumerable<ConsultaContactoSinMensajeDTO>> GetConsultasPorEstadoNoLeidaAsync(bool noLeida);
    }
}