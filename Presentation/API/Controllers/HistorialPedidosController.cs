using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Presentation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class HistorialPedidosController : ControllerBase
    {
        private readonly ConsultaPedidoService _consultaPedidoService;

        public HistorialPedidosController(ConsultaPedidoService consultaPedidoService)
        {
            _consultaPedidoService = consultaPedidoService;
        }

        [HttpGet]
        public async Task<IActionResult> ObterPedidos()
        {
            var usuarioId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var pedidos = await _consultaPedidoService.ObterPedidosPorUsuarioAsync(usuarioId);
            return Ok(pedidos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPedidoPorId(Guid id)
        {
            var pedido = await _consultaPedidoService.ObterPedidoPorIdAsync(id);
            if (pedido == null)
                return NotFound();

            return Ok(pedido);
        }
    }
}
