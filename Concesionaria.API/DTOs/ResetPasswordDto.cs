using System.ComponentModel.DataAnnotations;

namespace Concesionaria.API.DTOs
{
    public record ResetPasswordDto
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "El e-mail es requerido.")]
        [EmailAddress(ErrorMessage = "El e-mail no es válido.")]
        public string Email { get; set; } = string.Empty;
        [Required(AllowEmptyStrings = false, ErrorMessage = "El token es requerido.")]
        public string Token { get; set; } = string.Empty;
        [Required(AllowEmptyStrings = false, ErrorMessage = "La contraseña nueva debe tener 6 o más caracteres entre números y letras.")]
        public string NewPassword { get; set; } = string.Empty;
    }
}
