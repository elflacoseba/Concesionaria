namespace Concesionaria.Admin.DTOs
{
    public record ConsultaContactoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Mensaje { get; set; } = string.Empty;
        public DateTime FechaEnvio { get; set; } = DateTime.UtcNow;
        public bool NoLeida { get; set; }
        public DateTime? FechaLectura { get; set; } = null;
        /// <summary>
        /// Vehículo de interés del cliente.
        /// </summary>
        public string VehiculoInteres { get; set; } = string.Empty;

        /// <summary>
        /// Vehículo ofrecido por el cliente en permuta.
        /// </summary>
        public string VehiculoPermuta { get; set; } = string.Empty;
    }
}
