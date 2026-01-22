namespace CA_Domain.Entities
{
    public class Drink
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Recipe { get; set; }
        public List<Ingredient> Ingredients { get; set; } = new();
        public List<Rating> Ratings { get; set; } = new();
        public double AverageRating { get; set; } = 0.0;
        public bool Verified { get; set; } = false;
        public string? ImageURL { get; set; }
    }
}