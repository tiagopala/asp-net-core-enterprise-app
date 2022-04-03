using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EnterpriseApp.Pedido.Application.BackgroundServices
{
    public class OrderOrchestratorIntegrationHandler : IHostedService, IDisposable
    {
        private readonly ILogger<OrderOrchestratorIntegrationHandler> _logger;
        private Timer _timer;

        public OrderOrchestratorIntegrationHandler(ILogger<OrderOrchestratorIntegrationHandler> logger)
        {
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Order orchestrator started.");

            _timer = new Timer(ProccessOrder, null, TimeSpan.Zero, TimeSpan.FromSeconds(15));
            return Task.CompletedTask;
        }

        private void ProccessOrder(object state)
        {
            _logger.LogInformation("Processing orders.");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping timer.");

            _timer.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
