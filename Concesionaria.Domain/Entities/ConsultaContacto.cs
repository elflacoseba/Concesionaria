namespace Concesionaria.Domain.Entities
{
    public class ConsultaContacto
    {
        /// <summary>
        /// Identificador único de la consulta.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre del cliente que realiza la consulta.
        /// </summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>
        /// Apellido del cliente que realiza la consulta.
        /// </summary>
        public string Apellido { get; set; } = string.Empty;

        /// <summary>
        /// Correo electrónico de contacto del cliente.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Teléfono de contacto del cliente.
        /// </summary>
        public string Telefono { get; set; } = string.Empty;

        /// <summary>
        /// Mensaje enviado por el cliente en la consulta.
        /// </summary>
        public string Mensaje { get; set; } = string.Empty;

        /// <summary>
        /// Fecha y hora en que se envió la consulta.
        /// </summary>
        public DateTime FechaEnvio { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Indica si la consulta aún no ha sido leída.
        /// </summary>
        public bool NoLeida { get; set; } = true;

        /// <summary>
        /// Fecha y hora en que la consulta fue leída, si corresponde.
        /// </summary>
        public DateTime? FechaLectura { get; set; } = null;

        /// <summary>
        /// Vehículo de interés del cliente.
        /// </summary>
        public string? VehiculoInteres { get; set; } = string.Empty;

        /// <summary>
        /// Vehículo ofrecido por el cliente en permuta.
        /// </summary>
        public string? VehiculoPermuta { get; set; } = string.Empty;
    }
}