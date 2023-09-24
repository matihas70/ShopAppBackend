using ShopApp.Models;

namespace ShopApp.Interfaces
{
    public interface ICartService
    {
        void AddToCart(AddToCartDto dto, Guid sessionId);
        void UpdateItemQuantity(UpdateItemQuantityDto dto, Guid sessionId);
    }
}
