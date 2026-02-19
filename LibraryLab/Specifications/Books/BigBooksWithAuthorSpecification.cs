using LibraryLab.Databases.Entities;
using LibraryLab.Specifications.Base;

namespace LibraryLab.Specifications.Books
{
    public class BigBooksWithAuthorSpecification : Specification<Book>
    {
        public BigBooksWithAuthorSpecification(int pageCount = 500)
        {
            AddFilterQuering(book => book.PageCount > pageCount);
            AddIncludeQuery(book => book.Author);
        }
    }
}
