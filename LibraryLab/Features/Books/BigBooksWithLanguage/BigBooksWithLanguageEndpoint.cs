
using LibraryLab.Databases;
using LibraryLab.Extensions;
using LibraryLab.Specifications.Books;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryLab.Features.Books.BigBooksWithLanguage
{
    public class BigBooksWithLanguageEndpoint : IEndpoint
    {
        public void MapEndpoint(WebApplication app)
        {
            app.MapGet("api/books/big-books-with-language/{language}", Handle);
        }

        public static async Task<IResult> Handle(
            [FromRoute] string language,
            AppDbContenxt dbContext,
            ILogger<BigBooksWithLanguageEndpoint> logger,
            CancellationToken cancellationToken)
        {
            var langSpecification = new LanguageSpecification(language);
            var bigBookSpecification = new BigBooksWithAuthorSpecification();
            var combineSpecification = bigBookSpecification.And(langSpecification);

            var response = await dbContext.ApplySpecification(combineSpecification)
                .Select(x=>x.ToBookDto())
                .ToListAsync(cancellationToken);

            return Results.Ok(response);
        }
    }
}
