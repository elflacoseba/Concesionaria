using Concesionaria.Admin.Services.Interfaces;
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
    }
}
