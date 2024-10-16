using Application.Interfaces;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class OfertasController : ControllerBase
    {
        private readonly IOfertaService _ofertaService;

        public OfertasController(IOfertaService ofertaService)
        {
            _ofertaService = ofertaService;
        }

        [HttpGet]
        public async Task<IActionResult> ObterOfertasAtuais()
        {
            var ofertas = await _ofertaService.ObterOfertasAtuaisAsync();
            return Ok(ofertas);
        }
    }
}
