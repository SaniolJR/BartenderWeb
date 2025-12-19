namespace CA_Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Passwd { get; set; }
        public string Nick { get; set; }
        public string Email { get; set; }
        public List<Rating> Ratings { get; set; } = new();
        public List<Drink> FavouriteDrinks { get; set; } = new();

    }
}

