using Concesionaria.Admin.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Concesionaria.Admin.Pages.ConsultaContacto
{
    public class DetalleConsultaModel : PageModel
    {
        private readonly IConsultasContactoService _consultasContactoService;
        public Concesionaria.Admin.Models.ConsultaContacto? ConsultaDetalle { get; set; }

        public DetalleConsultaModel(IConsultasContactoService consultasContactoService)
        {
            _consultasContactoService = consultasContactoService;
        }

        public async Task OnGetAsync(int id)
        {
            ConsultaDetalle = await _consultasContactoService.GetConsultaContactoByIdAsync(id);
        }
    }
}
