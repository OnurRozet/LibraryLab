namespace LibraryLab.Databases.Entities
{
    public class Author
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }
        public int PublishedBook
        {
            get
            {
                return Books.Count;
            }
        }

        public ICollection<Book> Books { get; set; } = [];

    }
}
