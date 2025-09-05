using System.ComponentModel.DataAnnotations;

namespace Concesionaria.Admin.Models
{
    public class ConsultaContactoCreacion
    {
        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public string? Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido es requerido")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public string? Apellido { get; set; } = string.Empty;

        [Required(ErrorMessage = "El email es requerido")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        [EmailAddress(ErrorMessage = "Email no válido")]
        public string? Email { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "El teléfono es requerido")]
        [StringLength(20, ErrorMessage = "Máximo 20 caracteres")]
        public string? Telefono { get; set; } = string.Empty;

        [Required(ErrorMessage = "El mensaje es requerido")]
        [StringLength(2000, ErrorMessage = "Máximo 2000 caracteres")]
        public string? Mensaje { get; set; } = string.Empty;
    }
}
