using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Services
{
    internal class LibraryService
    {
        private readonly LibraryDbContext _dbContext;

        public LibraryService(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Book>> GetAllBooksAsync(DateTime startDate, DateTime endDate)
        {
            var books = await _dbContext.Books
                .Where(b => !b.Rentals.Any(rental =>
                    startDate < rental.EndDate && endDate >= rental.StartDate))
                .ToListAsync();
            return books;
        }

        public async Task<User> GetUserByName(string name)
        {
            var user = await _dbContext.Users
                .Where(u => u.FirstName.Contains(name) || u.LastName.Contains(name))
                .FirstOrDefaultAsync();
            return user;
        }

        public async Task<bool> IsBookAvailable(int bookId, DateTime startDate, DateTime endDate)
        {
            var isBookAvailable = await _dbContext.Books
                .Where(b => b.BookID == bookId)
                .AllAsync(b => !b.Rentals.Any
                (rental => startDate < rental.EndDate && endDate >= rental.StartDate));
            return isBookAvailable;
        }

        public async Task<Rental> AddRental(int bookId, int userId, DateTime startDate, DateTime endDate)
        {
            var rental = new Rental
            {
                BookID = bookId,
                UserID = userId,
                StartDate = startDate,
                EndDate = endDate
            };
            
            _dbContext.Rentals.Add(rental);
            await _dbContext.SaveChangesAsync();
            return rental;
        }

        public async Task<List<Book>> FindBook(string word)
        {
            var books = await _dbContext.Books
                .Where(b => b.Title.Contains(word) || b.Author.Contains(word))
                .ToListAsync();
            return books;
        }
    }
}
