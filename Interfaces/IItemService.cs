using ShopApp.Models;

namespace ShopApp.Interfaces
{
    public interface IItemService
    {
        List<GetCategoryDto> GetCategories();
    }
}
