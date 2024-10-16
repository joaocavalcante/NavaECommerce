using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Presentation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PedidosController : ControllerBase
    {
        private readonly PedidoService _pedidoService;

        public PedidosController(PedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        [HttpPost]
        public async Task<IActionResult> CriarPedido()
        {
            var usuarioId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var pedido = await _pedidoService.CriarPedidoAsync(usuarioId);
            return CreatedAtAction(nameof(ObterPedidoPorId), new { id = pedido.Id }, pedido);
        }

        [HttpPost("{pedidoId}/itens")]
        public async Task<IActionResult> AdicionarItem(Guid pedidoId, [FromBody] AdicionarItemDto dto)
        {
            await _pedidoService.AdicionarItemAsync(pedidoId, dto.ProdutoId, dto.Quantidade);
            return NoContent();
        }

        [HttpDelete("{pedidoId}/itens/{produtoId}")]
        public async Task<IActionResult> RemoverItem(Guid pedidoId, Guid produtoId)
        {
            await _pedidoService.RemoverItemAsync(pedidoId, produtoId);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPedidoPorId(Guid id)
        {
            // Implementar método para obter pedido
            return Ok();
        }

        // Outros endpoints como Confirmar Pedido, Cancelar Pedido, etc.
    }

    // DTOs
    public class AdicionarItemDto
    {
        public Guid ProdutoId { get; set; }
        public int Quantidade { get; set; }
    }
}
