using Concesionaria.Admin.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Concesionaria.Admin.Pages.ConsultaContacto
{
    [AutoValidateAntiforgeryToken]
    public class ConsultasContactoModel : PageModel
    {
        private readonly IConsultasContactoService _consultasContactoService;
        public IEnumerable<DTOs.ConsultaContactoDto>? ConsultasContacto { get; set; }

        public ConsultasContactoModel(IConsultasContactoService consultasContactoService)
        {
            _consultasContactoService = consultasContactoService;
        }

        public async Task OnGetAsync()
        {
            var token = Request.Cookies["AuthToken"];
            if (string.IsNullOrEmpty(token))
            {
                // Redirige al login si no hay token
                Response.Redirect("/Auth/SignIn");
                return;
            }

            ConsultasContacto = await _consultasContactoService.GetConsultasContactoAsync(token);
            ViewData["Breadcrumbs"] = new List<dynamic>
            {
                new { Nombre = "Inicio", Url = "/" },
                new { Nombre = "Consultas de Contacto", Url = "" }
            };
        }

        public async Task<IActionResult> OnPostMarcarLeidaAsync(int id, bool leida)
        {
            var token = Request.Cookies["AuthToken"];

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized();
            }

            var result = await _consultasContactoService.MarcarConsultaContactoLeidaByIdAsync(id, leida, token);
            if (result == true)
                return new JsonResult(new { success = true });
            return BadRequest();
        }
   
        public async Task<IActionResult> OnPostEliminarConsultaAsync(int id)
        {
            var token = Request.Cookies["AuthToken"];
            
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized();
            }

            var result = await _consultasContactoService.EliminarConsultaContactoByIdAsync(id, token);
            if (result == true)
                return new JsonResult(new { success = true });
            return BadRequest();
        }
    }
}
