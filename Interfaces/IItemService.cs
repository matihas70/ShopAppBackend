using ShopApp.Models;

namespace ShopApp.Interfaces
{
    public interface IItemService
    {
        List<OutputGetCategoryDto> GetCategories();
        List<OutputGetItemDto> GetItems(InputGetItemsDto dto);
        bool AddItem(AddItemDto dto);
    }
}
