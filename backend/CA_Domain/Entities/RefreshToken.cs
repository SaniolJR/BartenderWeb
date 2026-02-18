namespace CA_Domain.Entities
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public int UserId { get; set; }
        public User UserObj { get; set; }
        public DateTime ExpiresAt { get; set; }
        public bool IsRevoked { get; set; }

    }
}

