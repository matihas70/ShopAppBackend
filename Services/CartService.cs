using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using ShopApp.Entites;
using ShopApp.Exceptions;
using ShopApp.Interfaces;
using ShopApp.Models;
using System.Text.Json;

namespace ShopApp.Services
{
    public class CartService : ICartService
    {
        private readonly IDbContextFactory<ShopContext> dbFactory;
        private readonly IItemService itemService;
        public CartService(IDbContextFactory<ShopContext> _dbFactory, IItemService _itemService) 
        {
            dbFactory = _dbFactory;
            itemService = _itemService;
        }
        
        public void AddToCart(AddToCartDto dto, Guid sessionId)
        {
            using ShopContext db = dbFactory.CreateDbContext();

            CheckItemAvailability(dto.ItemId, dto.Size, dto.Quantity);
            
            Cart cart = db.Carts
                .Include(c => c.User)
                .ThenInclude(u => u.Sessions)
                .FirstOrDefault(c => c.User.Sessions.Any(s => s.Id == sessionId));

            List<CartItemsModel> items = JsonSerializer.Deserialize<List<CartItemsModel>>(cart.Items);
            CartItemsModel item = new CartItemsModel()
            {
                itemId = dto.ItemId,
                size = dto.Size,
                quantity = dto.Quantity
            };
            items.Add(item);

            cart.Items = JsonSerializer.Serialize(items);
            db.SaveChanges();
        }
        public void UpdateItemQuantity(UpdateItemQuantityDto dto, Guid sessionId)
        {
            using ShopContext db = dbFactory.CreateDbContext();
            Cart cart = db.Carts
                .Include(c => c.User)
                .ThenInclude(u => u.Sessions)
                .FirstOrDefault(c => c.User.Sessions.Any(s => s.Id == sessionId));

            List<CartItemsModel> items = JsonSerializer.Deserialize<List<CartItemsModel>>(cart.Items);

            CartItemsModel item = items.FirstOrDefault(i => i.itemId == dto.ItemId);
            CheckItemAvailability(dto.ItemId, dto.Size, dto.Count);

            if (dto.Count == 0)
            {
                items.Remove(item);
            }
            else
            {
                item.quantity = dto.Count;
            }
            cart.Items = JsonSerializer.Serialize(items);

            db.SaveChanges();

        }

        private void CheckItemAvailability(int itemId, string itemSize, int itemQuantity)
        {
            using ShopContext db = dbFactory.CreateDbContext();

            string sizesJson = db.ItemsStock.FirstOrDefault(i => i.Id == itemId)?.Stock;
            if (sizesJson == null)
                throw new ItemNotFoundException();

            List<SizeModel> sizes = JsonSerializer.Deserialize<List<SizeModel>>(sizesJson);

            if(sizes.FirstOrDefault(s => s.size == itemSize) == null)
                throw new ItemNotFoundException();

            int inStock = sizes.FirstOrDefault(s => s.size == itemSize).count;

            if (inStock > 0 && inStock < itemQuantity)
            {
                throw new ItemNotAvailableException(inStock);
            }
            else if (inStock == 0)
            {
                throw new ItemNotAvailableException();
            }
        }
        private List<CartItemsModel> GetItemsFromCart(Guid sessionId, ShopContext db)
        {
            Cart cart = db.Carts
                .Include(c => c.User)
                .ThenInclude(u => u.Sessions)
                .FirstOrDefault(c => c.User.Sessions.Any(s => s.Id == sessionId));

            return JsonSerializer.Deserialize<List<CartItemsModel>>(cart.Items);
        }
    }
}
