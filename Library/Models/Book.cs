namespace Library.Models
{
    internal class Book
    {
        public int BookID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }

        public ICollection<Rental> Rentals { get; set; }

        public Book()
        {
            Rentals = new List<Rental>();
        }
    }
}
