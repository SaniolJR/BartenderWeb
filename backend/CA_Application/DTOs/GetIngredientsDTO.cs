namespace CA_Application.DTOs
{
    public class GetIngredientsDTO
    {
        public string TextFilter { get; set; } = string.Empty;

        public int PageSize { get; set; } = 20;
        public int Page { get; set; } = 0;
    }
}