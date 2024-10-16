using Application.Interfaces;
using Domain.Entities;

namespace Application.Services
{
    public class OfertaService
    {
        private readonly IOfertaRepository _ofertaRepository;

        public OfertaService(IOfertaRepository ofertaRepository)
        {
            _ofertaRepository = ofertaRepository;
        }

        public async Task IniciarCampanhaBlackFridayAsync()
        {
            var agora = DateTime.UtcNow;
            for (int i = 0; i < 24; i++)
            {
                var horaDisponivel = agora.Date.AddHours(i);
                var oferta = new Oferta("iPhone", 100, 1.00m, horaDisponivel);
                await _ofertaRepository.AdicionarAsync(oferta);
            }
        }

        public async Task<IEnumerable<Oferta>> ObterOfertasAtuaisAsync()
        {
            var agora = DateTime.UtcNow;
            return await _ofertaRepository.ListarOfertasDisponiveisAsync(agora);
        }
    }
}
