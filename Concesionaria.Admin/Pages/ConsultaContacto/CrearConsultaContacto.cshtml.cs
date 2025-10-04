using Concesionaria.Admin.DTOs;
using Concesionaria.Admin.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Concesionaria.Admin.Pages.ConsultaContacto
{
    public class CrearConsultaContactoModel : PageModel
    {
        private readonly IConsultasContactoService _consultasContactoService;

        [BindProperty]
    public ConsultaContactoCreacionDto ConsultaContacto { get; set; } = null!;

        [TempData]
        public string? SuccessMessage { get; set; }
        [TempData]
        public string? ErrorMessage { get; set; }

        public CrearConsultaContactoModel(IConsultasContactoService consultasContactoService)
        {
            _consultasContactoService = consultasContactoService;
        }

        public void OnGet()
        {
            ConsultaContacto = new ConsultaContactoCreacionDto();
            ErrorMessage = null;
        }

        public async Task<ActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {                
                var result = await _consultasContactoService.CrearConsultaContactoAsync(ConsultaContacto);

                if (result == null)
                {
                    ErrorMessage = "Ocurrió un error al crear la consulta de contacto.";
                    return Page();
                }

                SuccessMessage = "La consulta se creó exitosamente.";
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                ErrorMessage = "Ha ocurrido un error inesperado: " + ex.Message;
                return Page();
            }
        }
    }
}
