namespace ShopApp.Models.Requests
{
    public class ItemVariantDto
    {
        public int ItemId { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public int Discount { get; set; } = 0;
        public List<SizeModel> Stock { get; set; }
    }
}
