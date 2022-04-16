using EnterpriseApp.Core.DomainObjects;
using EnterpriseApp.Core.Messages.Integration;
using EnterpriseApp.MessageBus;
using EnterpriseApp.Pagamento.API.Enums;
using EnterpriseApp.Pagamento.API.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EnterpriseApp.Pagamento.API.Services
{
    public class PaymentIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus _messageBus;
        private readonly IServiceProvider _serviceProvider;

        public PaymentIntegrationHandler(IMessageBus messageBus, IServiceProvider serviceProvider)
        {
            _messageBus = messageBus;
            _serviceProvider = serviceProvider;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetResponder();
            SetSubscribers();
            return Task.CompletedTask;
        }

        private void SetResponder()
        {
            _messageBus.RespondAsync<OrderInitializedIntegrationEvent, ResponseMessage>(async request => await AuthorizePayment(request));

            _messageBus.AdvancedBus.Connected += OnConnect;
        }

        private async Task<ResponseMessage> AuthorizePayment(OrderInitializedIntegrationEvent @event)
        {
            ResponseMessage responseMessage;

            var paymentRequest = new Models.Payment
            {
                CreditCard = new Models.CreditCard(@event.CardName, @event.CardNumber, @event.MonthYearDueDate, @event.CVV),
                OrderId = @event.OrderID,
                PaymentType = PaymentTypeEnum.CreditCard,
                Price = @event.Price
            };

            using (var scope = _serviceProvider.CreateScope())
            {
                var paymentService = scope.ServiceProvider.GetRequiredService<IPaymentService>();
                responseMessage = await paymentService.AuthorizePayment(paymentRequest);
            }

            return responseMessage;
        }

        private void SetSubscribers()
        {
            _messageBus.SubscribeAsync<CancelOrderIntegrationEvent>("CancelledOrder", async request => await CancelPayment(request));

            _messageBus.SubscribeAsync<WithdrawnOrderIntegrationEvent>("WithdrawnOrder", async request => await CapturePayment(request));
        }

        private async Task CancelPayment(CancelOrderIntegrationEvent request)
        {
            using var scope = _serviceProvider.CreateScope();
            var paymentService = scope.ServiceProvider.GetRequiredService<IPaymentService>();
            var response = await paymentService.CancelPayment(request.OrderId);

            if(!response.ValidationResult.IsValid)
                throw new DomainException($"Error while trying to cancel payment for this OrderId:{request.OrderId}");
        }

        private async Task CapturePayment(WithdrawnOrderIntegrationEvent request)
        {
            using var scope = _serviceProvider.CreateScope();
            var paymentService = scope.ServiceProvider.GetRequiredService<IPaymentService>();
            var response = await paymentService.CapturePayment(request.OrderId);

            if (!response.ValidationResult.IsValid)
                throw new DomainException($"Error while trying to capture payment for this OrderId:{request.OrderId}");

            await _messageBus.PublishAsync<OrderPaidIntegrationEvent>(new OrderPaidIntegrationEvent(request.CustomerId, request.OrderId));
        }

        private void OnConnect(object sender, EventArgs args)
            => SetResponder();
    }
}
