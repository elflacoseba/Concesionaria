using Concesionaria.Admin.DTOs;

namespace Concesionaria.Admin.Services.Interfaces
{
    public interface IUsuariosService
    {
        Task<RespuestaAutenticacionDto> LoginAsync(CredencialesUsuarioDto credencialesUsuarioDto);
    }
}