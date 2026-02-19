using LibraryLab.Databases.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryLab.Databases
{
    public class AppDbContenxt : DbContext
    {
        public AppDbContenxt(DbContextOptions<AppDbContenxt> options) : base(options)
        {
            
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.HasDefaultSchema(DBConst.DefaultShema);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContenxt).Assembly);
        }
        
    }
}
