namespace Library.Models
{
    internal class User
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<Rental> Rentals { get; set; }

        public User()
        {
            Rentals = new List<Rental>();
        }
    }
}
