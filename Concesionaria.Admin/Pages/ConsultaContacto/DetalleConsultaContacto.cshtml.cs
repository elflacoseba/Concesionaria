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
            ViewData["Breadcrumbs"] = new List<dynamic>
            {
                new { Nombre = "Inicio", Url = "/" },
                new { Nombre = "Consultas de Contacto", Url = "/consultas-contacto" },
                new { Nombre = "Detalle", Url = "" }
            };

            ConsultaContacto = await _consultasContactoService.GetConsultaContactoByIdAsync(id);

            if (ConsultaContacto != null && ConsultaContacto.NoLeida)
            {
                await _consultasContactoService.MarcarConsultaContactoLeidaByIdAsync(id, true);
                ConsultaContacto = await _consultasContactoService.GetConsultaContactoByIdAsync(id);
            }

            return Page();
        }

        public async Task<IActionResult> OnPostEliminarConsultaAsync(int id)
        {
            var result = await _consultasContactoService.EliminarConsultaContactoByIdAsync(id);
            if (result == true)
                return new JsonResult(new { success = true });
            return BadRequest();
        }
    }
}
