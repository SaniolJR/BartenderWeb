namespace CA_Application.DTOs
{
    public class GetDrinksDTO
    {

        public bool Verified { get; set; } = false;   //can in URL be as true, default is false
        public string? TextFilter { get; set; } = string.Empty;
        public int MissingIngredients { get; set; } = 0;
        public List<string> Ingredients { get; set; } = new List<string>();
        public int PageSize { get; set; } = 20;
        public int Page { get; set; } = 0;
    }

    public class DrinkDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Recipe { get; set; }
        public List<IngredientDTO> Ingredients { get; set; }
        public double AverageRating { get; set; }
        public bool Verified { get; set; }
        public string? ImageURL { get; set; }
    }

    public class IngredientDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}