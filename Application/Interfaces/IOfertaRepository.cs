using Domain.Entities;

namespace Application.Interfaces
{
    public interface IOfertaRepository
    {
        Task<IEnumerable<Oferta>> ListarOfertasDisponiveisAsync(DateTime currentTime);
        Task AdicionarAsync(Oferta oferta);
    }
}
