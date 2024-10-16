using Domain.Entities;

namespace Application.Interfaces
{
    public interface IPedidoRepository
    {
        Task<Pedido> ObterPorIdAsync(Guid id);
        Task<IEnumerable<Pedido>> ObterPorUsuarioAsync(Guid usuarioId);
        Task AdicionarAsync(Pedido pedido);
        Task AtualizarAsync(Pedido pedido);
    }
}
