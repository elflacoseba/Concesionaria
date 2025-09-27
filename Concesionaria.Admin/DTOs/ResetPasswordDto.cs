using System.ComponentModel.DataAnnotations;

namespace oncesionaria.Admin.DTOs
{
    public record ResetPasswordDto
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "El e-mail es requerido.")]
        [EmailAddress(ErrorMessage = "El e-mail no es válido.")]
        public string Email { get; set; } = string.Empty;
        [Required(AllowEmptyStrings = false, ErrorMessage = "El token es requerido.")]
        public string Token { get; set; } = string.Empty;
        [Required(AllowEmptyStrings = false, ErrorMessage = "La nueva contraseña es requerida.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{6,}$", ErrorMessage = "La contraseña debe tener al menos 6 caracteres, incluyendo números, letras minúsculas, mayúsculas y caracteres especiales.")]
        public string NewPassword { get; set; } = string.Empty;
    }
}
