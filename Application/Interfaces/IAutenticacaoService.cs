using Application.DTOs;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAutenticacaoService
    {
        Task<string> LoginAsync(string email, string senha);
        Task RegistrarAsync(RegistroDto dto);
    }
}
