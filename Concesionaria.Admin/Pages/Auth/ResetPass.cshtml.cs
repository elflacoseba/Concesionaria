using Concesionaria.Admin.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;

namespace Concesionaria.Admin.Pages.Auth
{
    public class ResetPassModel : PageModel
    {
        private readonly IUsuariosService _usuariosService;

        public string? SuccessMessage { get; set; }
        public string? ErrorMessage { get; set; }

        public ResetPassModel(IUsuariosService usuariosService)
        {
            _usuariosService = usuariosService;
        }

        public void OnGet() { }

        public async Task<IActionResult> OnPost(string email)
        {
            var result = await _usuariosService.GenerarResetPasswordToken(email);
            
            if (result == (int)HttpStatusCode.OK)
            {
                SuccessMessage = "¡El enlace para restablecer la contraseña fue enviado exitosamente!";
                return Page();
            }
            else if (result == (int)HttpStatusCode.NotFound)
            {
                ErrorMessage = "No se encontró un usuario con ese correo electrónico.";
                return Page();
            }

            ErrorMessage = "Ocurrió un error inesperado. Intenta nuevamente.";
            return Page();
         }

    }
}
