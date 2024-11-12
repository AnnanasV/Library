using Microsoft.EntityFrameworkCore;

namespace Library.Models
{
    internal class LibraryDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Rental> Rentals { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=E:\\Users\\user\\Documents\\Library.mdf;Integrated Security=True;Connect Timeout=30");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rental>()
                .HasKey(r => r.RentalID);

            modelBuilder.Entity<Rental>()
                .HasOne(r => r.Book)
                .WithMany(b => b.Rentals)
                .HasForeignKey(r => r.BookID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Rental>()
                .HasOne(r => r.User)
                .WithMany(u => u.Rentals)
                .HasForeignKey(r => r.UserID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
