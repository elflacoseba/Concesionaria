using System.IdentityModel.Tokens.Jwt;
namespace Concesionaria.Admin.Helpers
{
    public static class JwtHelper
    {
        public static (string Nombre, string Apellido) ObtenerNombreApellido(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);

            var nombre = jwt.Claims.FirstOrDefault(c => c.Type == "UsuarioNombre")?.Value ?? "";
            var apellido = jwt.Claims.FirstOrDefault(c => c.Type == "UsuarioApellido")?.Value ?? "";

            return (nombre, apellido);
        }
    }
}