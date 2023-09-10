using Microsoft.EntityFrameworkCore;
using ShopApp.Entites;
using ShopApp.Interfaces;
using ShopApp.Models;

namespace ShopApp.Services
{
    public class ItemService : IItemService
    {
        private readonly IDbContextFactory<ShopContext> dbFactory;
        public ItemService(IDbContextFactory<ShopContext> _dbFactory)
        {
            dbFactory = _dbFactory;
        }

        public List<GetCategoryDto> GetCategories()
        {
            using ShopContext db = dbFactory.CreateDbContext();

            List<Category> categories = db.Categories.Include(c => c.SubCategories).ToList();
            return categoriesToDtoModel(categories);
        }
        private List<GetCategoryDto> categoriesToDtoModel(List<Category> categories)
        {
            if (categories == null)
                return null;

            List<GetCategoryDto> dtoList = new List<GetCategoryDto>();
            foreach(Category category in categories)
            {
                GetCategoryDto dto = new GetCategoryDto()
                {
                    Id = category.Id,
                    Name = category.Name,
                    SubCategories = category.SubCategories == null ? null : categoriesToDtoModel(category.SubCategories.ToList())
                };
                dtoList.Add(dto);
            }
            return dtoList;

        }


    }
}
