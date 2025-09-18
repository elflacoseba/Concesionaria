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
            ConsultasContacto = await _consultasContactoService.GetConsultasContactoAsync();
            ViewData["Breadcrumbs"] = new List<dynamic>
            {
                new { Nombre = "Inicio", Url = "/" },
                new { Nombre = "Consultas de Contacto", Url = "" }
            };
        }

        public async Task<IActionResult> OnPostMarcarLeidaAsync(int id, bool leida)
        {
            var result = await _consultasContactoService.MarcarConsultaContactoLeidaByIdAsync(id, leida);
            if (result == true)
                return new JsonResult(new { success = true });
            return BadRequest();
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
