using Concesionaria.Admin.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Concesionaria.Admin.Pages.ConsultaContacto
{
    public class DetalleConsultaModalModel : PageModel
    {
        private readonly IConsultasContactoService _consultasContactoService;
        public Concesionaria.Admin.Models.ConsultaContacto? ConsultaDetalle { get; set; }

        public DetalleConsultaModalModel(IConsultasContactoService consultasContactoService)
        {
            _consultasContactoService = consultasContactoService;
        }

        public async Task OnGetAsync(int id)
        {
            ConsultaDetalle = await _consultasContactoService.GetConsultaContactoByIdAsync(id);
            
            if (ConsultaDetalle != null && ConsultaDetalle.NoLeida)
            {
                await _consultasContactoService.MarcarConsultaContactoLeidaByIdAsync(id, true);
                ConsultaDetalle = await _consultasContactoService.GetConsultaContactoByIdAsync(id);
            }

        }
    }
}
