using System.ComponentModel.DataAnnotations;

namespace Concesionaria.API.DTOs
{
    public record CredencialesUsuarioDto
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es obligatorio.")]
        [EmailAddress(ErrorMessage = "El campo {0} debe ser un correo electrónico válido.")]
        public string Email { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es obligatorio.")]
        public string Password { get; set; } = string.Empty;

    }
}
