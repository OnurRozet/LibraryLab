using LibraryLab.Databases;
using LibraryLab.Specifications;
using LibraryLab.Specifications.Base;
using Microsoft.EntityFrameworkCore;

namespace LibraryLab.Extensions
{
    public static class SpecificationExtension
    {
        public static IQueryable<T> ApplySpecification<T>(this AppDbContenxt dbContext, ISpecification<T> specification) where T : class
        {
            ArgumentNullException.ThrowIfNull(specification, nameof(specification));

            var defineSpecification = new DefineSpecification<T>(specification);

            var query = dbContext.Set<T>().AsNoTracking();
            query = defineSpecification.Define(query);
            return query;
        }

        public static async Task<List<T>> WhereAsync<T>(this AppDbContenxt dbContext, ISpecification<T> specification, CancellationToken cancellationToken = default) where T : class
        {
            ArgumentNullException.ThrowIfNull(specification, nameof(specification));
            cancellationToken.ThrowIfCancellationRequested();

            var defineSpecification = new DefineSpecification<T>(specification);

            var query = dbContext.Set<T>().AsNoTracking();
            query = defineSpecification.Define(query);
            return await query.ToListAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}
