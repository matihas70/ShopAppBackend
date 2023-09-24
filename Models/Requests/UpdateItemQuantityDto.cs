namespace ShopApp.Models
{
    public class UpdateItemQuantityDto
    {
        public int ItemId { get; set; }
        public int Count { get; set; }
        public string Size { get; set; }
    }
}
