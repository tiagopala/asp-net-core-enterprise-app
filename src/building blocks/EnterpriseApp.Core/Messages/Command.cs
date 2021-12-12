using FluentValidation.Results;
using MediatR;
using System;

namespace EnterpriseApp.Core.Messages
{
    public abstract class Command : Message, IRequest<ValidationResult>
    {
        public DateTime Timestamp { get; private set; }
        public ValidationResult ValidationResult { get; protected set; }

        public Command()
        {
            Timestamp = DateTime.Now;
        }

        public virtual bool Validate()
        {
            throw new NotImplementedException();
        }
    }
}
