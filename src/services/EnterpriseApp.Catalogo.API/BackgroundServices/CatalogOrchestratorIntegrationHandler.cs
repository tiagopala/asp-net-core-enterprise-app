using EnterpriseApp.Catalogo.API.Models;
using EnterpriseApp.Core.DomainObjects;
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
                CancelOrder(@event);
                return;
            }

            foreach (var product in products)
            {
                var quantidadeProduto = @event.Items.FirstOrDefault(p => p.Key == product.Id).Value;

                if (product.IsAvailable(quantidadeProduto))
                {
                    product.WithdrawFromStock(quantidadeProduto);
                    productAtStock.Add(product);
                }
            }

            if(productAtStock.Count != @event.Items.Count)
            {
                CancelOrder(@event);
                return;
            }

            foreach (var product in productAtStock)
                productRepository.Update(product);

            if (!await productRepository.UnitOfWork.Commit())
            {
                // Disclaire: Caso a exceção seja lançada, a mensagem será enviada para uma fila de erros a qual tentará ser reprocessada até ser processada com sucesso.
                throw new DomainException($"Problems happened while updating database for OrderId:{@event.OrderId}.");
            }

            var withdrawnOrder = new WithdrawnOrderIntegrationEvent(@event.CustomerId, @event.OrderId);

            await _messageBus.PublishAsync(withdrawnOrder);
        }

        private async void CancelOrder(OrderAuthorizedIntegrationEvent @event)
            => await _messageBus.PublishAsync(new CancelOrderIntegrationEvent(@event.CustomerId, @event.OrderId));

        private void SetResponder()
        {
            _messageBus.SubscribeAsync<OrderAuthorizedIntegrationEvent>("AuthorizedOrder", async request => await WithdrawFromStock(request));

            _messageBus.AdvancedBus.Connected += OnConnect;
        }

        private void OnConnect(object sender, EventArgs args)
            => SetResponder();
    }
}
