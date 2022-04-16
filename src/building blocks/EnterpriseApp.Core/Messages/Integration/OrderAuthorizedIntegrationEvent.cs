using System;
using System.Collections.Generic;

namespace EnterpriseApp.Core.Messages.Integration
{
    public class OrderAuthorizedIntegrationEvent : IntegrationEvent
    {
        public Guid OrderId { get; private set; }
        public Guid CustomerId { get; private set; }
        public IDictionary<Guid, int> Items { get; private set; }

        public OrderAuthorizedIntegrationEvent(Guid orderId, Guid customerId, IDictionary<Guid, int> items)
        {
            OrderId = orderId;
            CustomerId = customerId;
            Items = items;
        }
    }
}
