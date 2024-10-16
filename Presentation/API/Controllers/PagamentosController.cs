using Application.Services;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PagamentosController : ControllerBase
    {
        private readonly PagamentoService _pagamentoService;

        public PagamentosController(PagamentoService pagamentoService)
        {
            _pagamentoService = pagamentoService;
        }

        [HttpPost("{pedidoId}")]
        public async Task<IActionResult> IniciarPagamento(Guid pedidoId, [FromBody] IniciarPagamentoDto dto)
        {
            var pagamento = await _pagamentoService.IniciarPagamentoAsync(pedidoId, dto.TipoPagamento);
            return Ok(pagamento);
        }
    }

    // DTOs
    public class IniciarPagamentoDto
    {
        public TipoPagamento TipoPagamento { get; set; }
    }
}
