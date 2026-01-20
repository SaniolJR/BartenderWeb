namespace CA_Application.DTOs
{
    public class GetDrinksDTO
    {

        public bool Verified { get; set; } = false;   //can in URL be as true, default is false
        public string TextFilter { get; set; } = string.Empty;
        public int MissingIngredients { get; set; } = 0;
        public List<string> Ingredients { get; set; } = new List<string>();
    }
}