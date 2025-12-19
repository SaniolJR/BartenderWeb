namespace CA_Domain.Entities
{
    public class Drink
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Receipe { get; set; }
        public List<Ingredient> Ingredients { get; set; } = new();
        public List<Rating> Ratings { get; set; } = new();
    }
}