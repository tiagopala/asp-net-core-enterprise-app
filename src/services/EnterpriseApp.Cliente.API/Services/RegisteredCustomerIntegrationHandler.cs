using EnterpriseApp.Cliente.API.Application.Commands;
using EnterpriseApp.Core.Mediator;
using EnterpriseApp.Core.Messages.Integration;
using EnterpriseApp.MessageBus;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EnterpriseApp.Cliente.API.Services
{
    public class RegisteredCustomerIntegrationHandler : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IMessageBus _bus;

        public RegisteredCustomerIntegrationHandler(
            IServiceProvider serviceProvider,
            IMessageBus bus)
        {
            _serviceProvider = serviceProvider;
            _bus = bus;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _bus.RespondAsync<UserRegisteredIntegrationEvent, ResponseMessage>(async request => await RegisterCustomer(request));

            return Task.CompletedTask;
        }

        private async Task<ResponseMessage> RegisterCustomer(UserRegisteredIntegrationEvent integrationEvent)
        {
            ValidationResult result;
            var cmd = new RegisterCustomerCommand(integrationEvent.Id, integrationEvent.Name, integrationEvent.Email, integrationEvent.Cpf);

            using (var scope = _serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
                result = await mediator.SendCommand(cmd);
            }

            return new ResponseMessage(result);
        }
    }
}
