using LibraryLab.Databases.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryLab.Databases.Configurations
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.FirstName)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(a => a.LastName)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(a => a.BirthDate)
                .IsRequired();
                builder.HasMany(a => a.Books)
                .WithOne(b => b.Author)
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
