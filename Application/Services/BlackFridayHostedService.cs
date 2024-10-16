using Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services
{
    public class BlackFridayHostedService : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<BlackFridayHostedService> _logger;

        public BlackFridayHostedService(IServiceProvider serviceProvider, ILogger<BlackFridayHostedService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Black Friday Hosted Service iniciado.");

            // Iniciar a campanha imediatamente
            _timer = new Timer(ExecuteTask, null, TimeSpan.Zero, TimeSpan.FromHours(1));

            return Task.CompletedTask;
        }

        private async void ExecuteTask(object state)
        {
            _logger.LogInformation("Executando tarefa do Black Friday Hosted Service.");

            using (var scope = _serviceProvider.CreateScope())
            {
                var ofertaService = scope.ServiceProvider.GetRequiredService<IOfertaService>();
                try
                {
                    await ofertaService.IniciarCampanhaBlackFridayAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro ao iniciar a campanha Black Friday.");
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Black Friday Hosted Service parando.");

            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
