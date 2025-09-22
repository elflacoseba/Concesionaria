using Concesionaria.Admin.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Concesionaria.Admin.Pages.ConsultaContacto
{
    [AutoValidateAntiforgeryToken]
    public class DetalleConsultaContactoModel : PageModel
    {
        private readonly IConsultasContactoService _consultasContactoService;
    public DTOs.ConsultaContactoDto? ConsultaContacto { get; set; }

        public DetalleConsultaContactoModel(IConsultasContactoService consultasContactoService)
        {
            _consultasContactoService = consultasContactoService;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var token = Request.Cookies["AuthToken"];
            if (string.IsNullOrEmpty(token))
            {
                // Redirige al login si no hay token
                return Redirect("/Auth/SignIn");
            }

            ViewData["Breadcrumbs"] = new List<dynamic>
            {
                new { Nombre = "Inicio", Url = "/" },
                new { Nombre = "Consultas de Contacto", Url = "/consultas-contacto" },
                new { Nombre = "Detalle", Url = "" }
            };

            ConsultaContacto = await _consultasContactoService.GetConsultaContactoByIdAsync(id, token);

            if (ConsultaContacto != null && ConsultaContacto.NoLeida)
            {
                await _consultasContactoService.MarcarConsultaContactoLeidaByIdAsync(id, true, token);
                ConsultaContacto = await _consultasContactoService.GetConsultaContactoByIdAsync(id, token);
            }

            return Page();
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
