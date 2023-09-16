using ShopApp.Models;

namespace ShopApp.Interfaces
{
    public interface IItemService
    {
        List<OutputCategoryDto> GetCategories();
        List<OutputItemDto> GetItems(InputItemsDto dto);
    }
}
