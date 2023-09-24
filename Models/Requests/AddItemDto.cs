using ShopApp.Enums;
using ShopApp.Models.Requests;

namespace ShopApp.Models
{
    public class AddItemDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int BrandId { get; set; }
        public List<int> CategoriesId { get; set; }
        public byte Gender { get; set; }
        public List<ItemVariantDto> Variants { get; set; }
        public List<string> Sizes { get; set; }
    }
}
