using FluentValidation.Results;

namespace EnterpriseApp.Core.Extensions
{
    public static class ValidationResultExtensions
    {
        public static ValidationResult AddCustomError(this ValidationResult validationResult, string errorMessage)
        {
            validationResult.Errors.Add(new ValidationFailure(string.Empty, errorMessage));
            return validationResult;
        }
    }
}
