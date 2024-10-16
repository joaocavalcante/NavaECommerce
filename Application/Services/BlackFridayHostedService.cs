using Application.Services;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class BlackFridayHostedService : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly OfertaService _ofertaService;

        public BlackFridayHostedService(OfertaService ofertaService)
        {
            _ofertaService = ofertaService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // Iniciar a campanha imediatamente
            _ofertaService.IniciarCampanhaBlackFridayAsync();

            // Agendar verificações a cada hora
            _timer = new Timer(VerificarOfertas, null, TimeSpan.Zero, TimeSpan.FromHours(1));

            return Task.CompletedTask;
        }

        private void VerificarOfertas(object state)
        {
            // Lógica para adicionar novas ofertas a cada hora, se necessário
            _ofertaService.IniciarCampanhaBlackFridayAsync();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
