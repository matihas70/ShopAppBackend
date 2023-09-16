using Microsoft.EntityFrameworkCore;
using ShopApp.Entites;
using ShopApp.Interfaces;
using ShopApp.Models;
using System.Diagnostics;
using System.Linq.Expressions;

namespace ShopApp.Services
{
    public class ItemService : IItemService
    {
        private readonly IDbContextFactory<ShopContext> dbFactory;
        public ItemService(IDbContextFactory<ShopContext> _dbFactory)
        {
            dbFactory = _dbFactory;
        }

        public List<OutputItemDto> GetItems(InputItemsDto dto)
        {
            using ShopContext db = dbFactory.CreateDbContext();

            IQueryable<Category> query = db.Categories.Where(c => dto.CategoriesId.Any(x => x == c.Id))
                    .Select(GetSubCategories(4));

            List<Category> categories = query.ToList();

            List<int> categoriesId = GetCategoriesId(categories);
            
            var res = db.Items.Include(i => i.Categories)
                .Where(i => i.Gender == dto.Gender && i.Categories.Any(y => categoriesId.Contains(y.Id)))
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                    x.Description,
                    Categories = x.Categories == null ? null : x.Categories.Select(y => y.Id),
                    x.BrandId,
                    x.Gender
                }).AsEnumerable()
            .Select(x =>
            {
                return new OutputItemDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    CategoriesId = x.Categories.ToList(),
                    BrandId = x.BrandId,
                    Gender = x.Gender
                };

            }).ToList();
            return res;
        }
        private List<int> GetCategoriesId(List<Category> categories)
        {
            List<int> result = new List<int>();
            foreach (var category in categories)
            {
                result.Add(category.Id);
                result = result.Concat(GetCategoriesId(category.SubCategories.ToList())).ToList();
            }
            return result;
        }

        private Expression<Func<Category, Category>> GetSubCategories(int maxDepth, int curDepth = 0)
        {
            curDepth++;
            Expression<Func<Category, Category>> result = category => new Category()
            {
                Id = category.Id,
                SubCategories = curDepth == maxDepth ? new List<Category>() : category.SubCategories.AsQueryable()
                .Where(c => c.SuperCategoryId == category.Id)
                .Select(GetSubCategories(maxDepth, curDepth)).ToList()
            };

            return result;
        }

        public List<OutputCategoryDto> GetCategories()
        {
            using ShopContext db = dbFactory.CreateDbContext();

            List<Category> categories = db.Categories.ToList();
            List<Category> mainCategories = categories.Where(c => c.SuperCategoryId == null).ToList();

            var test = categoriesToDtoModel(mainCategories);
            return categoriesToDtoModel(mainCategories);
        }
        private List<OutputCategoryDto> categoriesToDtoModel(List<Category> categories)
        {
            if (categories == null)
                return null;

            List<OutputCategoryDto> dtoList = new List<OutputCategoryDto>();
            foreach (Category category in categories)
            {
                OutputCategoryDto dto = new OutputCategoryDto()
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
