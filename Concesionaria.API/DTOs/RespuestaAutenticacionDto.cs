namespace Concesionaria.API.DTOs
{
    public class RespuestaAutenticacionDto
    {
        public required string Token { get; set; } = string.Empty;
        public DateTime Expiracion { get; set; } = DateTime.MinValue;
    }
}
