namespace Library.Models
{
    internal class Rental
    {
        public int RentalID { get; set; }

        public int BookID { get; set; }
        public Book Book { get; set; }

        public int UserID { get; set; }
        public User User { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
