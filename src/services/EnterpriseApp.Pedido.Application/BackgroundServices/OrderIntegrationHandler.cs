using EnterpriseApp.Core.DomainObjects;
using EnterpriseApp.Core.Messages.Integration;
using EnterpriseApp.MessageBus;
using EnterpriseApp.Pedido.Domain.Pedidos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EnterpriseApp.Pedido.Application.BackgroundServices
{
    public class OrderIntegrationHandler : BackgroundService
    {
        private Timer _timer;
        private readonly IMessageBus _messageBus;
        private readonly IServiceProvider _serviceProvider;

        public OrderIntegrationHandler(IMessageBus messageBus, IServiceProvider serviceProvider)
        {
            _messageBus = messageBus;
            _serviceProvider = serviceProvider;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(SetSubscribers, null, TimeSpan.Zero, TimeSpan.FromSeconds(15));

            return Task.CompletedTask;
        }

        private void SetSubscribers(object obj)
        {
            if (!_messageBus.AdvancedBus.IsConnected)
            {
                _messageBus.SubscribeAsync<CancelOrderIntegrationEvent>("CancelOrder", async request => await CancelOrder(request));

                _messageBus.SubscribeAsync<OrderPaidIntegrationEvent>("OrderPaid", async request => await FinishOrder(request));
            }
        }

        private async Task CancelOrder(CancelOrderIntegrationEvent request)
        {
            using var scope = _serviceProvider.CreateScope();
            var orderRepository = scope.ServiceProvider.GetRequiredService<IOrderRepository>();

            var order = await orderRepository.GetById(request.OrderId);
            order.CancelOrder();

            orderRepository.Update(order);

            if(!await orderRepository.UnitOfWork.Commit())
                throw new DomainException($"Error while trying to cancel order for this OrderId:{request.OrderId}");
        }

        private async Task FinishOrder(OrderPaidIntegrationEvent request)
        {
            using var scope = _serviceProvider.CreateScope();
            var orderRepository = scope.ServiceProvider.GetRequiredService<IOrderRepository>();

            var order = await orderRepository.GetById(request.OrderId);
            order.FinishOrder();

            orderRepository.Update(order);

            if (!await orderRepository.UnitOfWork.Commit())
                throw new DomainException($"Error while trying to finish order for this OrderId:{request.OrderId}");
        }
    }
}
