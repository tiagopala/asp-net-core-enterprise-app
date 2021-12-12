using FluentValidation.Results;

namespace EnterpriseApp.Core.Messages
{
    public abstract class CommandHandlerBase
    {
        protected ValidationResult ValidationResult;

        public CommandHandlerBase()
            => ValidationResult = new();

        public void AddCustomError(string message)
            => ValidationResult.Errors.Add(new ValidationFailure(string.Empty, message));
    }
}
