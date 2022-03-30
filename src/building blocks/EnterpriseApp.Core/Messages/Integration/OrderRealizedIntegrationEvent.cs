using System;

namespace EnterpriseApp.Core.Messages.Integration
{
    public class OrderRealizedIntegrationEvent : IntegrationEvent
    {
        public Guid CustomerId { get; set; }

        public OrderRealizedIntegrationEvent(Guid customerId)
        {
            CustomerId = customerId;
        }
    }
}
