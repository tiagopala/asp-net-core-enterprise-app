using EnterpriseApp.Core.Messages.Integration;
using EnterpriseApp.MessageBus;
using EnterpriseApp.Pedido.Application.Queries;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EnterpriseApp.Pedido.Application.BackgroundServices
{
    public class OrderOrchestratorIntegrationHandler : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly ILogger<OrderOrchestratorIntegrationHandler> _logger;
        private readonly IServiceProvider _serviceProvider;

        public OrderOrchestratorIntegrationHandler(
            IServiceProvider serviceProvider,
            ILogger<OrderOrchestratorIntegrationHandler> logger)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(ProccessOrder, null, TimeSpan.Zero, TimeSpan.FromSeconds(30));
            return Task.CompletedTask;
        }

        private async void ProccessOrder(object state)
        {
            _logger.LogInformation("Processing orders.");

            using (var scope = _serviceProvider.CreateScope())
            {
                var orderQueries = scope.ServiceProvider.GetRequiredService<IOrderQueries>();
                var authorizedOrder = await orderQueries.GetAuthorizedOrders();

                if (authorizedOrder is null)
                    return;

                var messageBus = scope.ServiceProvider.GetRequiredService<IMessageBus>();

                var orderAuthorizedEvent = new OrderAuthorizedIntegrationEvent(authorizedOrder.Id, authorizedOrder.CustomerId, authorizedOrder.OrderItems.ToDictionary(x => x.ProductId, x => x.Quantity));

                await messageBus.PublishAsync(orderAuthorizedEvent);

                _logger.LogInformation($"OrderId: {authorizedOrder.Id} was sent to withdraw from stock.");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping timer.");

            _timer.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
            => _timer?.Dispose();
    }
}
