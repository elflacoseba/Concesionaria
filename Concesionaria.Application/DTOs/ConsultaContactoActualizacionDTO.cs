namespace Concesionaria.Application.DTOs
{
    public record ConsultaContactoActualizacionDTO
    {        
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Mensaje { get; set; } = string.Empty;     
    }
}
