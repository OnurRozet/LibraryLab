using LibraryLab.Databases.Entities;
using LibraryLab.Specifications.Base;

namespace LibraryLab.Specifications.Books
{
    public class LanguageSpecification : Specification<Book>
    {
        public LanguageSpecification(string language = "tr")
        {
            AddFilterQuering(x => x.Language.ToLower() == language.ToLower());

        }
    }
}
