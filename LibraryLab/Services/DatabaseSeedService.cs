using Bogus;
using LibraryLab.Databases;
using LibraryLab.Databases.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryLab.Services
{
    public class DatabaseSeedService
    {

        public static async Task SeedDatabaseAsync(AppDbContenxt dbContext, int count)
        {
            await dbContext.Database.MigrateAsync();
            if(dbContext.Books.Any() || dbContext.Authors.Any() || dbContext.Categories.Any())
            {
                return; 
            }
            var authors = await SeedAuthorsAsync(dbContext, count);
            var categories = await SeedCategoriesAsync(dbContext, count);
            var books = await SeedBooksAsync(dbContext, count);
            await dbContext.SaveChangesAsync();
        }

        private static async Task<List<Book>> SeedBooksAsync(AppDbContenxt dbContext, int count)
        {
            var locales = new[] { "en", "fr", "de", "es", "it" };
            var bookFaker = new Faker<Book>()
                .RuleFor(b => b.Title, f => f.Lorem.Sentence(3, 5))
                .RuleFor(b => b.AuthorId, f => f.Random.Int(1, count))
                .RuleFor(b => b.CategoryId, f => f.Random.Int(1, count))
                .RuleFor(b => b.ISBN, f => f.Random.Replace("###-#-##-######-#"))
                .RuleFor(b => b.Price, f => f.Random.Decimal(5, 100))
                .RuleFor(b => b.PageCount, f => f.Random.Int(100, 1000))
                .RuleFor(b => b.StockCount, f => f.Random.Int(0, 50))
                .RuleFor(b => b.PublishedDate, f => f.Date.Past(20))
                .RuleFor(b => b.IsDigital, f => f.Random.Bool())
                .RuleFor(b => b.Rating, f => f.Random.Double(1, 5))
                .RuleFor(b => b.Language, f => f.Random.ArrayElement<string>(locales));


            var books = bookFaker.Generate(count);
            dbContext.Books.AddRange(books);
            await dbContext.SaveChangesAsync();
            return books;
        }


        private static async Task<List<Author>> SeedAuthorsAsync(AppDbContenxt dbContext, int count)
        {
            var authorFaker = new Faker<Author>()
                .RuleFor(a => a.FirstName, f => f.Name.FirstName())
                .RuleFor(a => a.LastName, f => f.Name.LastName())
                .RuleFor(a => a.BirthDate, f => f.Date.Past(50, DateTime.Now.AddYears(-20)));
            var authors = authorFaker.Generate(count);
            dbContext.Authors.AddRange(authors);
            await dbContext.SaveChangesAsync();
            return authors;
        }

        private static async Task<List<Category>> SeedCategoriesAsync(AppDbContenxt dbContext, int count)
        {
            var categoryFaker = new Faker<Category>()
                .RuleFor(c => c.Name, f => f.Commerce.Categories(1)[0]);
            var categories = categoryFaker.Generate(count);
            dbContext.Categories.AddRange(categories);
            await dbContext.SaveChangesAsync();
            return categories;
        }
    }
}
