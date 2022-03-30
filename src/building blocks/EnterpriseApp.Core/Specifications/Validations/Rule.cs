namespace EnterpriseApp.Core.Specifications.Validations
{
    public class Rule<T>
    {
        public string ErrorMessage { get; }
        private readonly Specification<T> _specificationSpec;

        public Rule(Specification<T> spec, string errorMessage)
        {
            _specificationSpec = spec;
            ErrorMessage = errorMessage;
        }

        public bool Validate(T obj)
        {
            return _specificationSpec.IsSatisfiedBy(obj);
        }
    }
}
