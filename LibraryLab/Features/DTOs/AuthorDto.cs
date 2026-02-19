using LibraryLab.Databases.Entities;

namespace LibraryLab.Features.DTOs
{
    public class AuthorDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public int PublishedBook { get; set; }

    }
}
