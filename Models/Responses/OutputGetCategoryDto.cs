namespace ShopApp.Models
{
    public class OutputGetCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<OutputGetCategoryDto> SubCategories { get; set; }
    }
}
