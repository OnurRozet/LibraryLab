using LibraryLab.Databases.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryLab.Databases.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(b => b.ISBN)
                .IsRequired()
                .HasMaxLength(20);
            builder.Property(b => b.Price)
                .HasColumnType("decimal(18,2)");
            builder.Property(b => b.PublishedDate)
                .HasColumnType("date");
            builder.Property(b=> b.Rating)
                .HasColumnType("float");
            builder.Property(b=> b.StockCount)
                .HasDefaultValue(0);
             builder.HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(b => b.Category)
                .WithMany(c => c.Books)
                .HasForeignKey(b => b.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
