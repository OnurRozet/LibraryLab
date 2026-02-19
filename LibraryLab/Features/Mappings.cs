using LibraryLab.Databases.Entities;
using LibraryLab.Features.DTOs;

namespace LibraryLab.Features
{
    public static class Mappings
    {
        public static BookDto ToBookDto(this Book book)
        {
            return new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                AuthorId = book.AuthorId,
                CategoryId = book.CategoryId,
                ISBN = book.ISBN,
                Price = book.Price,
                PageCount = book.PageCount,
                StockCount = book.StockCount,
                PublishedDate = book.PublishedDate,
                IsDigital = book.IsDigital,
                Rating = book.Rating,
                Language = book.Language
            };
        }

        public static CategoryDto ToCategoryDto(this Category category)
        {
            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        public static AuthorDto ToAuthorDto(this Author author)
        {
            return new AuthorDto
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName,
                BirthDate = author.BirthDate,
                PublishedBook = author.PublishedBook
            };
        }
    }
}
