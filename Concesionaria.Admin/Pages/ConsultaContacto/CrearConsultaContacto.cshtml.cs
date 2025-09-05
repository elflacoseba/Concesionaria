using Concesionaria.Admin.Models;
using Concesionaria.Admin.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Concesionaria.Admin.Pages.ConsultaContacto
{
    public class CrearConsultaContactoModel : PageModel
    {
        private readonly IConsultasContactoService _consultasContactoService;

        [BindProperty]
        public ConsultaContactoCreacion ConsultaContacto { get; set; } = null!;

        public CrearConsultaContactoModel(IConsultasContactoService consultasContactoService)
        {
            _consultasContactoService = consultasContactoService;
        }

        public void OnGet()
        {
            ConsultaContacto = new ConsultaContactoCreacion();
        }

        public async Task<ActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = await _consultasContactoService.CrearConsultaContactoAsync(ConsultaContacto);
            
            if (result == null)
            {
                ModelState.AddModelError(string.Empty, "Ocurrió un error al crear la consulta de contacto.");
                return Page();
            }

            return RedirectToPage("/ConsultaContacto/ConsultasContacto");
        }
    }
}
