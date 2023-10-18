using ShopApp.Enums;

namespace ShopApp.Models
{
    public class InputGetItemsDto
    {   
        public GenderEnum? Gender { get; set; }
        public List<int> CategoriesId { get; set; }
    }
}
