using Library.Models;

namespace Library.Services
{
    public static class DBInitializer
    {
        internal static void Seed(LibraryDbContext context)
        {
            if (context.Books.Any() || context.Users.Any())
            {
                return;
            }

            // Add books
            var books = new[]
            {
                new Book { Title = "To Kill a Mockingbird", Author = "Harper Lee", ISBN = "9780060935467" },
                new Book { Title = "1984", Author = "George Orwell", ISBN = "9780451524935" },
                new Book { Title = "Pride and Prejudice", Author = "Jane Austen", ISBN = "9781503290563" }
            };
            context.Books.AddRange(books);

            // Add users
            var users = new[]
            {
                new User { FirstName = "John", LastName = "Doe" },
                new User { FirstName = "Jane", LastName = "Smith" },
                new User { FirstName = "Emily", LastName = "Johnson" }
            };
            context.Users.AddRange(users);

            context.SaveChanges();

            // Add rentals
            var rentals = new[]
            {
                new Rental { BookID = books[0].BookID, UserID = users[0].UserID, StartDate = DateTime.Now.AddDays(-10), EndDate = DateTime.Now.AddDays(-5) },
                new Rental { BookID = books[1].BookID, UserID = users[1].UserID, StartDate = DateTime.Now.AddDays(-7), EndDate = DateTime.Now.AddDays(-2) },
                new Rental { BookID = books[2].BookID, UserID = users[2].UserID, StartDate = DateTime.Now.AddDays(-3), EndDate = DateTime.Now.AddDays(2) }
            };
            context.Rentals.AddRange(rentals);

            context.SaveChanges();
        }
    }
}
