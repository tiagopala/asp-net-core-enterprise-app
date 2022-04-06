using EnterpriseApp.Catalogo.API.Models;
using EnterpriseApp.Core.Messages.Integration;
using EnterpriseApp.MessageBus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EnterpriseApp.Catalogo.API.BackgroundServices
{
    public class CatalogOrchestratorIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus _messageBus;
        private readonly IServiceProvider _serviceProvider;

        public CatalogOrchestratorIntegrationHandler(
            IMessageBus messageBus, 
            IServiceProvider serviceProvider)
        {
            _messageBus = messageBus;
            _serviceProvider = serviceProvider;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetResponder();
            return Task.CompletedTask;
        }

        private async Task WithdrawFromStock(OrderAuthorizedIntegrationEvent @event)
        {
            var productAtStock = new List<Product>();
            using var scope = _serviceProvider.CreateScope();
            var productRepository = scope.ServiceProvider.GetRequiredService<IProductRepository>();
            var idProducts = string.Join(",", @event.Items.Select(i => i.Key));
            var products = await productRepository.GetProductsById(idProducts);

            if (products.Count() != @event.Items.Count)
            {

            }

            foreach (var product in products)
            {

            }

            if(productAtStock.Count != @event.Items.Count)
            {

            }

            foreach (var product in productAtStock)
            {

            }

            if (!await productRepository.UnitOfWork.Commit())
            {

            }

            var withdrawnOrder = new WithdrawnOrderIntegrationEvent(@event.CustomerId, @event.OrderId);

            await _messageBus.PublishAsync(withdrawnOrder);
        }

        private void SetResponder()
        {
            _messageBus.SubscribeAsync<OrderAuthorizedIntegrationEvent>("AuthorizedOrder", async request => await WithdrawFromStock(request));

            _messageBus.AdvancedBus.Connected += OnConnect;
        }

        private void OnConnect(object sender, EventArgs args)
            => SetResponder();
    }
}
