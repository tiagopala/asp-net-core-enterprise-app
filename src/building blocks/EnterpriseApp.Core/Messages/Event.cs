using MediatR;
using System;

namespace EnterpriseApp.Core.Messages
{
    public class Event : Message, INotification
    {
        public DateTime Timestamp { get; set; }
        public Event()
        {
            Timestamp = DateTime.Now;
        }
    }
}
