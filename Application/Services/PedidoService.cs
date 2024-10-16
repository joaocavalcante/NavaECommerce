using Application.Interfaces;
using Domain.Entities;
using Domain.Exceptions;

namespace Application.Services
{
    public class PedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IProdutoRepository _produtoRepository;

        public PedidoService(IPedidoRepository pedidoRepository, IProdutoRepository produtoRepository)
        {
            _pedidoRepository = pedidoRepository;
            _produtoRepository = produtoRepository;
        }

        public async Task<Pedido> CriarPedidoAsync(Guid usuarioId)
        {
            var pedido = new Pedido(usuarioId);
            await _pedidoRepository.AdicionarAsync(pedido);
            return pedido;
        }

        public async Task AdicionarItemAsync(Guid pedidoId, Guid produtoId, int quantidade)
        {
            var pedido = await _pedidoRepository.ObterPorIdAsync(pedidoId);
            if (pedido == null)
                throw new DomainException("Pedido não encontrado.");

            var produto = await _produtoRepository.ObterPorIdAsync(produtoId);
            if (produto == null)
                throw new DomainException("Produto não encontrado.");

            if (produto.Estoque < quantidade)
                throw new DomainException("Estoque insuficiente.");

            pedido.AdicionarItem(produtoId, quantidade, produto.Preco);
            produto.RemoverEstoque(quantidade);

            await _pedidoRepository.AtualizarAsync(pedido);
            await _produtoRepository.AtualizarAsync(produto);
        }

        public async Task RemoverItemAsync(Guid pedidoId, Guid produtoId)
        {
            var pedido = await _pedidoRepository.ObterPorIdAsync(pedidoId);
            if (pedido == null)
                throw new DomainException("Pedido não encontrado.");

            var item = pedido.Itens.Find(i => i.ProdutoId == produtoId);
            if (item == null)
                throw new DomainException("Item não encontrado no pedido.");

            var produto = await _produtoRepository.ObterPorIdAsync(produtoId);
            if (produto == null)
                throw new DomainException("Produto não encontrado.");

            produto.AdicionarEstoque(item.Quantidade);
            pedido.RemoverItem(produtoId);

            await _pedidoRepository.AtualizarAsync(pedido);
            await _produtoRepository.AtualizarAsync(produto);
        }

        // Outros métodos como ConfirmarPedido, CancelarPedido, etc.
    }
}
