namespace ShopApp.Models
{
    public class OutputCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<OutputCategoryDto> SubCategories { get; set; }
    }
}
