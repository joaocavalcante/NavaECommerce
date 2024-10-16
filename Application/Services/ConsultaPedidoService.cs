using Application.Interfaces;
using Domain.Entities;

namespace Application.Services
{
    public class ConsultaPedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;

        public ConsultaPedidoService(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<IEnumerable<Pedido>> ObterPedidosPorUsuarioAsync(Guid usuarioId)
        {
            return await _pedidoRepository.ObterPorUsuarioAsync(usuarioId);
        }

        public async Task<Pedido> ObterPedidoPorIdAsync(Guid pedidoId)
        {
            return await _pedidoRepository.ObterPorIdAsync(pedidoId);
        }
    }
}
