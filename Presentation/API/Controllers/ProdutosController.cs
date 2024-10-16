using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrador")]
    public class ProdutosController : ControllerBase
    {
        private readonly ProdutoService _produtoService;

        public ProdutosController(ProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpPost]
        public async Task<IActionResult> CriarProduto([FromBody] CriarProdutoDto dto)
        {
            var produto = await _produtoService.CriarProdutoAsync(dto.Nome, dto.Descricao, dto.Preco, dto.Estoque);
            return CreatedAtAction(nameof(ObterProdutoPorId), new { id = produto.Id }, produto);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ListarProdutos()
        {
            var produtos = await _produtoService.ListarProdutosAsync();
            return Ok(produtos);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> ObterProdutoPorId(Guid id)
        {
            var produto = await _produtoService.ObterPorIdAsync(id);
            if (produto == null)
                return NotFound();

            return Ok(produto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarProduto(Guid id, [FromBody] AtualizarProdutoDto dto)
        {
            await _produtoService.AtualizarProdutoAsync(id, dto.Nome, dto.Descricao, dto.Preco);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoverProduto(Guid id)
        {
            await _produtoService.RemoverProdutoAsync(id);
            return NoContent();
        }
    }

    // DTOs
    public class CriarProdutoDto
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public int Estoque { get; set; }
    }

    public class AtualizarProdutoDto
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
    }
}
