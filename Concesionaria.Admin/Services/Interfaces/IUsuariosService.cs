using Concesionaria.Admin.DTOs;
using oncesionaria.Admin.DTOs;

namespace Concesionaria.Admin.Services.Interfaces
{
    public interface IUsuariosService
    {
        Task<int> GenerarResetPasswordToken(string email);
        Task<RespuestaAutenticacionDto> LoginAsync(CredencialesUsuarioDto credencialesUsuarioDto);
        Task<int> ResetearPassword(ResetPasswordDto resetPasswordDto);
    }
}