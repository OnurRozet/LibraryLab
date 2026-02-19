
using LibraryLab.Databases;
using LibraryLab.Extensions;
using LibraryLab.Specifications.Books;
using Microsoft.EntityFrameworkCore;

namespace LibraryLab.Features.Books.BigBooksWithAuthor
{
    public class BigBooksWithAuthorEndpoint : IEndpoint
    {
        public void MapEndpoint(WebApplication app)
        {
            app.MapGet("api/books/big-books-with-author", Handle);
        }

        private static async Task<IResult> Handle(
            AppDbContenxt dbContext,
            ILogger<BigBooksWithAuthorEndpoint> logger,
            CancellationToken cancellationToken)
        {
            var specification = new BigBooksWithAuthorSpecification();
            var response = await dbContext.ApplySpecification(specification)
                .Select(x => x.ToBookDto())
                .ToListAsync(cancellationToken);

            logger.LogInformation("Sayfa sayısı 500 den büyük olan kitaplar yazarı ile birlikte getirildi");

            return Results.Ok(response);
        }
    }
}
