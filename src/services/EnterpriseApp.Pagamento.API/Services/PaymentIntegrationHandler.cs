using EnterpriseApp.Core.Messages.Integration;
using EnterpriseApp.MessageBus;
using EnterpriseApp.Pagamento.API.Enums;
using EnterpriseApp.Pagamento.API.Models;
using EnterpriseApp.Pagamento.API.Services.Interfaces;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
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
            return Task.CompletedTask;
        }

        private void SetResponder()
            => _messageBus.RespondAsync<OrderInitializedIntegrationEvent, ResponseMessage>((x) => AuthorizePayment(x));

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

            using var scope = _serviceProvider.CreateScope();
            var paymentService = scope.ServiceProvider.GetRequiredService<IPaymentService>();
            responseMessage = await paymentService.AuthorizePayment(paymentRequest);
            return responseMessage;
        }
    }
}
