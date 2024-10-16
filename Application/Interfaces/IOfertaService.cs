using Domain.Entities;

namespace Application.Interfaces
{
    public interface IOfertaService
    {
        Task IniciarCampanhaBlackFridayAsync();
        Task<IEnumerable<Oferta>> ObterOfertasAtuaisAsync();
    }
}
