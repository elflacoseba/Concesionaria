using Concesionaria.Admin.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Concesionaria.Admin.Pages.ConsultaContacto
{
    public class ConsultasContactoModel : PageModel
    {
        private readonly IConsultasContactoService _consultasContactoService;
        public IEnumerable<Models.ConsultaContacto>? ConsultasContacto { get; set; }

        public ConsultasContactoModel(IConsultasContactoService consultasContactoService)
        {
            _consultasContactoService = consultasContactoService;
        }

        public async Task OnGetAsync()
        {
            ConsultasContacto = await _consultasContactoService.GetConsultasContactoAsync();
        }

        public async Task<IActionResult> OnPostMarcarLeidaAsync(int id, bool leida)
        {
            var result = await _consultasContactoService.MarcarConsultaContactoLeidaByIdAsync(id, leida);
            if (result == true)
                return new JsonResult(new { success = true });
            return BadRequest();
        }
    }
}
