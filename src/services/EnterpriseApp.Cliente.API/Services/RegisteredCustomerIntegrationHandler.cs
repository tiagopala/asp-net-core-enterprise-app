using EasyNetQ;
using EnterpriseApp.Cliente.API.Application.Commands;
using EnterpriseApp.Core.Mediator;
using EnterpriseApp.Core.Messages.Integration;
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
        private IBus _bus;

        public RegisteredCustomerIntegrationHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _bus = RabbitHutch.CreateBus("host=localhost:5672");

            _bus.Rpc.RespondAsync<UserRegisteredIntegrationEvent, ResponseMessage>(async request => new ResponseMessage(await RegisterCustomer(request)));

            return Task.CompletedTask;
        }

        private async Task<ValidationResult> RegisterCustomer(UserRegisteredIntegrationEvent integrationEvent)
        {
            ValidationResult result;
            var cmd = new RegisterCustomerCommand(integrationEvent.Id, integrationEvent.Name, integrationEvent.Email, integrationEvent.Cpf);

            using (var scope = _serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
                result = await mediator.SendCommand(cmd);
            }

            return result;
        }
    }
}
