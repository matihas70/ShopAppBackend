namespace ShopApp.Models
{
    public class OutputGetItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; } 
        public string Description { get; set; }
        public List<int> CategoriesId { get; set; }
        public int BrandId { get; set; }
        public byte Gender { get; set; }
        
    }
}
