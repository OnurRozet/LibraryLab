using System.Linq.Expressions;

namespace LibraryLab.Specifications.Base
{
    public class OrSpecification<T> : Specification<T>  where T : class
    {

        public OrSpecification(Specification<T> left, Specification<T> right)
        {
            RegisterFilteringQuery(left, right);
        }
        private void RegisterFilteringQuery(Specification<T> left, Specification<T> right)
        {
            if (left.FilterQuery is null && right.FilterQuery is null)
            {
                return;
            }
            if (left.FilterQuery is null && right.FilterQuery is not null)
            {
                AddFilterQuering(right.FilterQuery!);
                return;
            }
            if (right.FilterQuery is null && left.FilterQuery is not null)
            {
                AddFilterQuering(left.FilterQuery);
                return;
            }
            else
            {
                var replaceParameter = new ReplaceSpecificationVisitor(left.FilterQuery!.Parameters[0], right.FilterQuery!.Parameters[0]);
                var replacedBody = replaceParameter.Visit(right.FilterQuery.Body);

                var orElseExpression = Expression.OrElse(replacedBody!, left.FilterQuery.Body);
                var combinedExpression = Expression.Lambda<Func<T, bool>>(orElseExpression, left.FilterQuery.Parameters.Single());
                AddFilterQuering(combinedExpression);
            }
        }
    }
}
