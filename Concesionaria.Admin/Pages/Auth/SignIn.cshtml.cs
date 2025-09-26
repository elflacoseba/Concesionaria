using Concesionaria.Admin.DTOs;
using Concesionaria.Admin.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Text.Json;

namespace Concesionaria.Admin.Pages.Auth
{
    public class SignInModel : PageModel
    {
        private readonly IUsuariosService _usuariosService;

        [BindProperty]
        public string? Email { get; set; }
        [BindProperty]
        public string? Password { get; set; }
        public string? ErrorMessage { get; set; }

        public SignInModel(IUsuariosService usuariosService)
        {
            _usuariosService = usuariosService;
        }        

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()              
        {
            if (!ModelState.IsValid)
            {
                ErrorMessage = "Datos inválidos.";
                return Page();
            }
            
            var credenciales = new CredencialesUsuarioDto { Email = Email!, Password = Password! };

            var response = await _usuariosService.LoginAsync(credenciales);
                        
            if (response != null)
            {                
                 Response.Cookies.Append("AuthToken", response.Token!);
                return Redirect("/");
            }
            else
            {
                ErrorMessage = "Credenciales incorrectas. Intenta nuevamente.";
                return Page();
            }
        }
    }
}
