namespace Concesionaria.Domain.Entities
{
    public class ConsultaContacto
    {
        /// <summary>
        /// Identificador �nico de la consulta.
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
        /// Correo electr�nico de contacto del cliente.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Tel�fono de contacto del cliente.
        /// </summary>
        public string Telefono { get; set; } = string.Empty;

        /// <summary>
        /// Mensaje enviado por el cliente en la consulta.
        /// </summary>
        public string Mensaje { get; set; } = string.Empty;

        /// <summary>
        /// Fecha y hora en que se envi� la consulta.
        /// </summary>
        public DateTime FechaEnvio { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Indica si la consulta a�n no ha sido le�da.
        /// </summary>
        public bool NoLeida { get; set; } = true;

        /// <summary>
        /// Fecha y hora en que la consulta fue le�da, si corresponde.
        /// </summary>
        public DateTime? FechaLectura { get; set; } = null;

        /// <summary>
        /// Veh�culo de inter�s del cliente.
        /// </summary>
        public string? VehiculoInteres { get; set; } = string.Empty;

        /// <summary>
        /// Veh�culo ofrecido por el cliente en permuta.
        /// </summary>
        public string? VehiculoPermuta { get; set; } = string.Empty;
    }
}