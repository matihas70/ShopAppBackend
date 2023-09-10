namespace ShopApp.Models
{
    public class GetCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<GetCategoryDto> SubCategories { get; set; }
    }
}
