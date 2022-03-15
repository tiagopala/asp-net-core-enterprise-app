using EnterpriseApp.Core.Messages.Integration;
using EnterpriseApp.MessageBus;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EnterpriseApp.Pedido.Application.Events
{
    public class OrderEventHandler : INotificationHandler<OrderRealizedEvent>
    {
        private readonly IMessageBus _messageBus;

        public OrderEventHandler(IMessageBus messageBus)
        {
            _messageBus = messageBus;
        }

        public async Task Handle(OrderRealizedEvent notification, CancellationToken cancellationToken)
        {
            await _messageBus.PublishAsync<OrderRealizedIntegrationEvent>(new OrderRealizedIntegrationEvent(notification.CustomerId));
        }
    }
}
