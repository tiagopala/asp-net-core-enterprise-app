using System;

namespace EnterpriseApp.Core.Messages.Integration
{
    public class WithdrawnOrderIntegrationEvent : IntegrationEvent
    {
        public Guid CustomerId { get; private set; }
        public Guid OrderId { get; private set; }

        public WithdrawnOrderIntegrationEvent(Guid customerId, Guid orderId)
        {
            CustomerId = customerId;
            OrderId = orderId;
        }
    }
}
