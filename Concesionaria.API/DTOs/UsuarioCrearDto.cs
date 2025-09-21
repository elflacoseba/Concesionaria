using System.ComponentModel.DataAnnotations;

namespace Concesionaria.API.DTOs
{
    public record UsuarioCrearDto
    {
        [Required(AllowEmptyStrings =false, ErrorMessage = "El nombre de usuario es obligatorio.")]
        public string UserName { get; set; } = string.Empty;
        
        [Required(AllowEmptyStrings = false, ErrorMessage = "El email es obligatorio.")]
        [EmailAddress(ErrorMessage = "El email no es válido.")]
        public string Email { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false, ErrorMessage = "La contraseña es obligatoria.")]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
        public string Password { get; set; } = string.Empty;
    }
}
