using Microsoft.EntityFrameworkCore;
using RXApp.Model;

namespace RXApp
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = true;
        }
        public ApplicationDbContext()
        {
           
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Reader> Readers { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<History> Histories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Связь между книгой и выдачей (многие-ко-многим)
            modelBuilder.Entity<Book>()
                .HasMany(book => book.Issues)
                .WithOne(issue => issue.Book)
                .HasForeignKey(issue => issue.BookId);

            // Связь между читателем и выдачей (многие-ко-многим)
            modelBuilder.Entity<Reader>()
                .HasMany(reader => reader.Issues)
                .WithOne(issue => issue.Reader)
                .HasForeignKey(issue => issue.ReaderId);

            modelBuilder.Entity<Issue>()
                .HasMany(i => i.Histories)
                .WithOne(he => he.Issue)
                .HasForeignKey(he => he.IssueId);
        }
    }
    
}
