using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario> ObterPorEmailAsync(string email);
        Task AdicionarAsync(Usuario usuario);
    }
}
