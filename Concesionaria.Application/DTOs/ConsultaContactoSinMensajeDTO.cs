namespace Concesionaria.Application.DTOs
{
    public record  ConsultaContactoSinMensajeDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public DateTime FechaEnvio { get; set; } = DateTime.UtcNow;
        public bool NoLeida { get; set; }
        public DateTime? FechaLectura { get; set; } = null;
        public string? VehiculoInteres { get; set; } = string.Empty;
        public string? VehiculoPermuta { get; set; } = string.Empty;
    }
}
