using Domain.Entities;

namespace Application.Interfaces
{
    public interface IProdutoRepository
    {
        Task<Produto> ObterPorIdAsync(Guid id);
        Task<IEnumerable<Produto>> ListarAsync();
        Task AdicionarAsync(Produto produto);
        Task AtualizarAsync(Produto produto);
        Task RemoverAsync(Guid id);
    }
}
