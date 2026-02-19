using System.Linq.Expressions;

namespace LibraryLab.Specifications.Base
{
    public class AndSpecification<T> : Specification<T> where T : class
    {
        public AndSpecification(Specification<T> left, Specification<T> right)
        {
            RegisterFilteringQuery(left, right);
        }

        private void RegisterFilteringQuery(Specification<T> left, Specification<T> right)
        {
            if(left.FilterQuery is null && right.FilterQuery is null)
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
                var replaceVisitor = new ReplaceSpecificationVisitor(right.FilterQuery.Parameters.Single(), left.FilterQuery.Parameters.Single());
                var replacedBody = replaceVisitor.Visit(right.FilterQuery.Body);

                var andExpression = Expression.AndAlso(left.FilterQuery.Body,replacedBody);
                var lambda = Expression.Lambda<Func<T, bool>>(andExpression, left.FilterQuery.Parameters.Single());
                AddFilterQuering(lambda);
            }
        }
    }
}
