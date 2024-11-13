using Library.Models;
using Library.Services;

namespace Library
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var dbContext = new LibraryDbContext())
            {
                //DBInitializer.Seed(dbContext); // Initialize data

                var libraryService = new LibraryService(dbContext);

                RunMenuAsync(libraryService).GetAwaiter().GetResult();
            }
        }

        private static async Task RunMenuAsync(LibraryService libraryService)
        {
            string word = string.Empty;
            int bookId;
            DateTime startDate = DateTime.Now;
            DateTime endDate = DateTime.Now.AddDays(7);
            List<Book> books;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\n--- Library service ---");
                Console.WriteLine("1. Available books");
                Console.WriteLine("2. Find book");
                Console.WriteLine("3. Rent book");
                Console.WriteLine("0. Exit");
                Console.WriteLine("\n");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ChangeDate(ref startDate, ref endDate);
                        PrintBookList(await libraryService.GetAllBooksAsync(startDate, endDate));
                        break;
                    case "2":
                        Console.WriteLine("Enter keyword/author/title:");
                        word = Console.ReadLine() ?? string.Empty;
                        PrintBookList(await libraryService.FindBook(word));
                        break;
                    case "3":
                        ChangeDate(ref startDate, ref endDate);
                        bookId = await GetBookId(libraryService, startDate, endDate);
                        await libraryService.AddRental(bookId, await GetUserId(libraryService), startDate, endDate);
                        Console.WriteLine("Success");
                        break;
                    case "0":
                        Console.WriteLine("Exit");
                        return;
                    default:
                        Console.WriteLine("NaN");
                        break;
                }
                Console.Write("Tap any key to continue...");
                Console.ReadKey();
            }
        }

        private static async Task<int> GetBookId(LibraryService libraryService, DateTime startDate, DateTime endDate)
        {
            int bookId = 0;
            while (true)
            {
                Console.WriteLine("Enter book id:");
                int.TryParse(Console.ReadLine(), out bookId);

                if (await libraryService.IsBookAvailable(bookId, startDate, endDate))
                {
                    return bookId;
                }
            }
        }

        private static async Task<int> GetUserId(LibraryService libraryService)
        {
            User user;
            int userId = 0;
            do
            {
                Console.WriteLine("Enter your name:");
                user = await libraryService.GetUserByName(Console.ReadLine());
                if (user == null)
                {
                    Console.WriteLine("User not found.");
                    continue;
                }
                userId = user.UserID;
            } while (userId == 0);

            return userId;
        }

        private static void ChangeDate(ref DateTime startDate, ref DateTime endDate)
        {
            Console.WriteLine($"Do you want to leave this date: {startDate} - {endDate} ?");
            Console.WriteLine("1. Yes");
            Console.WriteLine("2. No");
            if (Console.ReadLine() == "1")
            {
                return;
            }
            Console.WriteLine("Do you want book only for today?\n1. Yes\n2. No");
            bool isStartOk, isEndOk;
            do
            {
                Console.WriteLine("Enter start and and date:");
                isStartOk = DateTime.TryParse(Console.ReadLine(), out startDate);
                isEndOk = DateTime.TryParse(Console.ReadLine(), out endDate);
            } while (!isStartOk && !isEndOk);
        }

        private static void PrintBookList(List<Book> books)
        {
            if(books.Count == 0)
            {
                Console.WriteLine("0 books is available on this date");
                return;
            }
            foreach (var book in books)
            {
                Console.WriteLine($"{book.BookID}. {book.Title} - {book.Author}\n\tISBN {book.ISBN}");
            }
        }
    }
}