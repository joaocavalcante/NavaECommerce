using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;

namespace Application.Services
{
    public class PagamentoService
    {
        private readonly IPagamentoRepository _pagamentoRepository;
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IProdutoRepository _produtoRepository;

        public PagamentoService(IPagamentoRepository pagamentoRepository, IPedidoRepository pedidoRepository, IProdutoRepository produtoRepository)
        {
            _pagamentoRepository = pagamentoRepository;
            _pedidoRepository = pedidoRepository;
            _produtoRepository = produtoRepository;
        }

        public async Task<Pagamento> IniciarPagamentoAsync(Guid pedidoId, TipoPagamento tipo)
        {
            var pedido = await _pedidoRepository.ObterPorIdAsync(pedidoId);
            if (pedido == null)
                throw new DomainException("Pedido não encontrado.");

            if (pedido.Status != StatusPedido.Criado && pedido.Status != StatusPedido.PagamentoPendente)
                throw new DomainException("Pedido não está no estado correto para pagamento.");

            var pagamento = new Pagamento(pedidoId, pedido.ValorTotal, tipo);
            await _pagamentoRepository.AdicionarAsync(pagamento);

            pedido.AtualizarStatus(StatusPedido.PagamentoPendente);
            await _pedidoRepository.AtualizarAsync(pedido);

            // Integrar com gateway de pagamento aqui (Pix ou Cartão)
            // Simulação:
            bool pagamentoAprovado = SimularPagamento(pagamento);

            if (pagamentoAprovado)
            {
                pagamento.ConfirmarPagamento();
                pedido.AtualizarStatus(StatusPedido.PagamentoConfirmado);
            }
            else
            {
                pagamento.CancelarPagamento();
                pedido.AtualizarStatus(StatusPedido.Cancelado);
            }

            await _pagamentoRepository.AtualizarAsync(pagamento);
            await _pedidoRepository.AtualizarAsync(pedido);

            return pagamento;
        }

        private bool SimularPagamento(Pagamento pagamento)
        {
            // Simulação de aprovação de pagamento
            return true; // Sempre aprova para fins de exemplo
        }
    }
}
