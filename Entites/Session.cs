namespace ShopApp.Entites
{
    public class Session
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
