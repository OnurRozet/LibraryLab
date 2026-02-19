using System.Linq.Expressions;

namespace LibraryLab.Specifications.Base
{
    public class Specification<T> : ISpecification<T> where T : class
    {
        private List<Expression<Func<T, object>>> _orderByQueries = new();
        private List<Expression<Func<T, object>>> _includeQueries = new();
        private List<Expression<Func<T, object>>> _orderByDescendingQueries = new();
        private Expression<Func<T, bool>>? _filterQuery;

        protected Specification()
        {
        }

        protected Specification(Expression<Func<T, bool>> query)
        {
            _filterQuery = query;
        }

        protected Specification(ISpecification<T> specification)
        {
            _filterQuery = specification.FilterQuery;
            _orderByQueries = specification.OrderByQueries.ToList();
            _includeQueries = specification.IncludeQueries.ToList();
            _orderByDescendingQueries = specification.OrderByDescendingQueries.ToList();
        }

        public Expression<Func<T, bool>>? FilterQuery => _filterQuery;

        public IReadOnlyCollection<Expression<Func<T, object>>> IncludeQueries => _includeQueries;

        public IReadOnlyCollection<Expression<Func<T, object>>> OrderByQueries => _orderByQueries;

        public IReadOnlyCollection<Expression<Func<T, object>>> OrderByDescendingQueries => _orderByDescendingQueries;

        protected void AddFilterQuering(Expression<Func<T, bool>> query)
        {
            _filterQuery = query;
        }

        protected void AddIncludeQuery(Expression<Func<T, object>> query)
        {
            _includeQueries ??= new();
            _includeQueries.Add(query);
        }

        protected void AddOrderByQuery(Expression<Func<T, object>> query)
        {
            _orderByQueries ??= new();
            _orderByQueries.Add(query);
        }

        protected void AddOrderByDescendingQuery(Expression<Func<T, object>> query)
        {
            _orderByDescendingQueries ??= new();
            _orderByDescendingQueries.Add(query);
        }

        public Specification<T> And(Specification<T> specification)
        {
            return new AndSpecification<T>(this, specification);
        }

        public Specification<T> Or(Specification<T> specification)
        {
            return new OrSpecification<T>(this, specification);
        }
    }
}
