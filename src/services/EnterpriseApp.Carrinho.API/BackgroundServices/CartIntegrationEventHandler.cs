using EnterpriseApp.Carrinho.API.Data;
using EnterpriseApp.Core.Messages.Integration;
using EnterpriseApp.MessageBus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EnterpriseApp.Carrinho.API.BackgroundServices
{
    public class CartIntegrationEventHandler : BackgroundService
    {
        private Timer _timer;
        private readonly IMessageBus _messageBus;
        private readonly IServiceProvider _serviceProvider;

        public CartIntegrationEventHandler(
            IMessageBus messageBus,
            IServiceProvider serviceProvider)
        {
            _messageBus = messageBus;
            _serviceProvider = serviceProvider;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(SetSubscriber, null, TimeSpan.Zero, TimeSpan.FromSeconds(15));
            return Task.CompletedTask;
        }

        private void SetSubscriber(object obj)
        {
            if (!_messageBus.AdvancedBus.IsConnected)
            {
                _messageBus.SubscribeAsync<OrderRealizedIntegrationEvent>("OrderRealized", async request => await DeleteCart(request));
                _messageBus.AdvancedBus.Connected += OnConnect;
            }
        }

        private async Task DeleteCart(OrderRealizedIntegrationEvent @event)
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ShoppingCartContext>();

            var cart = await context.CartCustomer.FirstOrDefaultAsync(x => x.CustomerId == @event.CustomerId);

            if (cart is not null)
            {
                context.CartCustomer.Remove(cart);
                await context.SaveChangesAsync();
            }
        }

        private void OnConnect(object sender, EventArgs args)
            => SetSubscriber(null);
    }
}
