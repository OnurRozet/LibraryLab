using System.Linq.Expressions;

namespace LibraryLab.Specifications.Base
{
    public interface ISpecification<T> where T : class
    {
        Expression<Func<T, bool>>? FilterQuery { get; }
        IReadOnlyCollection<Expression<Func<T, object>>>? IncludeQueries { get; }
        IReadOnlyCollection<Expression<Func<T, object>>>? OrderByQueries { get; }
        IReadOnlyCollection<Expression<Func<T, object>>>? OrderByDescendingQueries { get; }
    }
}
