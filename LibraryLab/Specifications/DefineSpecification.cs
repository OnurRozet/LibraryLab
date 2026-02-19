using LibraryLab.Specifications.Base;
using Microsoft.EntityFrameworkCore;

namespace LibraryLab.Specifications
{
    public class DefineSpecification<T> : Specification<T> where T : class
    {
        public DefineSpecification(ISpecification<T> specification) : base(specification)
        {
            
        }

        public virtual IQueryable<T> Define(IQueryable<T> query)
        {
            if(FilterQuery is not null)
            {
                query = query.Where(FilterQuery);
            }

            if(IncludeQueries.Count > 0)
            {
                query = IncludeQueries.Aggregate(query, (current, include) => current.Include(include));
            }

            if(OrderByQueries.Count > 0)
            {
                var orderedQuerable = query.OrderBy(OrderByQueries.First());
                orderedQuerable = OrderByQueries.Skip(1).Aggregate(orderedQuerable, (current, orderBy) => current.ThenBy(orderBy));
                query = orderedQuerable;
            }

            if(OrderByDescendingQueries.Count > 0)
            {
                var orderByDescQuerable = query.OrderByDescending(OrderByDescendingQueries.First());
                orderByDescQuerable = OrderByDescendingQueries.Skip(1).Aggregate(orderByDescQuerable, (current, orderBy) => current.ThenByDescending(orderBy));
                query = orderByDescQuerable;
            }
            return query;
        }
    }
}
