using System;
using System.Linq.Expressions;

namespace EnterpriseApp.Core.Specifications
{
    public abstract class Specification<T>
    {
        private static readonly Specification<T> All = new IdentitySpecification<T>();

        public abstract Expression<Func<T, bool>> ToExpression();

        public Specification<T> And(Specification<T> specification)
        {
            if (this == All)
                return specification;
            if (specification == All)
                return this;

            return new AndSpecification<T>(this, specification);
        }

        public bool IsSatisfiedBy(T entity)
        {
            var predicate = ToExpression().Compile();
            return predicate(entity);
        }
    }
}
