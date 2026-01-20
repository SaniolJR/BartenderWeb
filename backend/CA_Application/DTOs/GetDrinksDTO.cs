namespace CA_Application.DTOs
{
    public class GetDrinksDTO
    {

        public bool Verified { get; set; } = false;   //can in URL be as true, default is false
        public string? TextFilter { get; set; }
        public int? MissingIngredients { get; set; }
        public List<string>? Ingredients { get; set; }
    }
}