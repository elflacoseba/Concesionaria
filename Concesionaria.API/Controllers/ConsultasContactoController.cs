using Concesionaria.Application.DTOs;
using Concesionaria.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Concesionaria.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultasContactoController : ControllerBase
    {
        private readonly IConsultaContactoService _consultaContactoService;

        public ConsultasContactoController(IConsultaContactoService consultaContactoService)
        {
            _consultaContactoService = consultaContactoService;
        }

        [HttpGet]
        public async Task<IEnumerable<ConsultaContactoDTO>> Get()
        {
            return await _consultaContactoService.GetAllConsultasContactoAsync();
        }
    }
}
