namespace CA_Domain.Entities
{
    public class Rating
    {
        public int Id { get; set; }
        public Date uploadDate { get; set; }
        public User Autor { get; set; }
        public Drink CertainDrink { get; set; }
        public string Text { get; set; }
        public int Stars { get; set; }

    }
}