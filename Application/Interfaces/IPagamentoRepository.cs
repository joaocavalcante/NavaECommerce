using Domain.Entities;

namespace Application.Interfaces
{
    public interface IPagamentoRepository
    {
        Task<Pagamento> ObterPorIdAsync(Guid id);
        Task AdicionarAsync(Pagamento pagamento);
        Task AtualizarAsync(Pagamento pagamento);
    }
}
