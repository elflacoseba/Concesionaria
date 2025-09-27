using Concesionaria.Admin.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using oncesionaria.Admin.DTOs;
using System.ComponentModel.DataAnnotations;

namespace Concesionaria.Admin.Pages.Auth
{
    public class NewPassModel : PageModel
    {
        private readonly IUsuariosService _usuariosService;

        public bool ShowInvalidTokenAlert { get; set; }
        public string? MessageResult { get; set; }
        public bool AlertDanger { get; set; } = false;
       
        [BindProperty]
        public ResetPasswordDto ResetPasswordModel { get; set; } = new ResetPasswordDto();
        
        [BindProperty]        
        public string ConfirmPassword { get; set; } = string.Empty;

        public NewPassModel(IUsuariosService usuariosService)
        {
            _usuariosService = usuariosService;
        }

        public void OnGet(string token, string email)
        {
            if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(email))
            {
                ShowInvalidTokenAlert = true;
            }

            ResetPasswordModel.Token = token;
            ResetPasswordModel.Email = email;                      
        }

        public async Task<IActionResult> OnPostAsync()
        {

            if (ResetPasswordModel.NewPassword != ConfirmPassword)
            {
                ModelState.AddModelError("NewPassword", "Las contraseñas no coincide.");
            }

            if (!ModelState.IsValid)
            {
                var errores = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .Where(msg => !string.IsNullOrWhiteSpace(msg));
                MessageResult = string.Join(" ", errores);
                AlertDanger = true;
                return Page();
            }
            var status = await _usuariosService.ResetearPassword(ResetPasswordModel);
            if (status == StatusCodes.Status200OK)
            {
                MessageResult = "Contraseña actualizada correctamente!. ";
                AlertDanger = false;
            }
            else
            {
                MessageResult = "No se pudo actualizar la contraseña.";
                AlertDanger = true;
            }
            return Page();
        }
    }
}
