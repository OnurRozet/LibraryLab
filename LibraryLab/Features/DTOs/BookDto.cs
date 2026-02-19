using LibraryLab.Databases.Entities;

namespace LibraryLab.Features.DTOs
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public int CategoryId { get; set; }
        public string ISBN { get; set; }
        public decimal Price { get; set; }
        public int PageCount { get; set; }
        public int StockCount { get; set; }
        public DateTime PublishedDate { get; set; }
        public bool IsDigital { get; set; } // E-kitap mı?
        public double Rating { get; set; } // 0-5 arası puan
        public string Language { get; set; } // "TR", "EN" vb.
    }
}
