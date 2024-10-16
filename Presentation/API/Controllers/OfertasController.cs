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
        private readonly OfertaService _ofertaService;

        public OfertasController(OfertaService ofertaService)
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
