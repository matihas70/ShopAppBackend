using Microsoft.EntityFrameworkCore;
using ShopApp.Entites;
using ShopApp.Enums;
using ShopApp.Interfaces;
using ShopApp.Models;
using ShopApp.Models.Requests;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Text.Json;

namespace ShopApp.Services
{
    public class ItemService : IItemService
    {
        private readonly IDbContextFactory<ShopContext> dbFactory;
        public ItemService(IDbContextFactory<ShopContext> _dbFactory)
        {
            dbFactory = _dbFactory;
        }

        public List<OutputGetItemDto> GetItems(InputGetItemsDto dto)
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
                return new OutputGetItemDto()
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

        public List<OutputGetCategoryDto> GetCategories()
        {
            using ShopContext db = dbFactory.CreateDbContext();

            List<Category> categories = db.Categories.ToList();
            List<Category> mainCategories = categories.Where(c => c.SuperCategoryId == null).ToList();

            var test = categoriesToDtoModel(mainCategories);
            return categoriesToDtoModel(mainCategories);
        }
        private List<OutputGetCategoryDto> categoriesToDtoModel(List<Category> categories)
        {
            if (categories == null)
                return null;

            List<OutputGetCategoryDto> dtoList = new List<OutputGetCategoryDto>();
            foreach (Category category in categories)
            {
                OutputGetCategoryDto dto = new OutputGetCategoryDto()
                {
                    Id = category.Id,
                    Name = category.Name,
                    SubCategories = category.SubCategories == null ? null : categoriesToDtoModel(category.SubCategories.ToList())
                };
                dtoList.Add(dto);
            }
            return dtoList;
        }

        public bool AddItem(AddItemDto dto)
        {
            using ShopContext db = dbFactory.CreateDbContext();
            // List<Category> categories = dto.CategoriesId.Select(c => new Category() { Id = c }).ToList();
            if (db.Items.FirstOrDefault(i => i.Name == dto.Name) != null)
                return false;

            
            Item newItem = new Item()
            {
                Name = dto.Name,
                Description = dto.Description,
                BrandId = dto.BrandId,
                Gender = dto.Gender,
                Sizes = JsonSerializer.Serialize(dto.Sizes)
            };

            List<ItemStock> itemsStock = new List<ItemStock>();

            List<SizeModel> sizesModel = new List<SizeModel>();
            foreach (string clothSize in dto.Sizes)
            {
                sizesModel.Add(new SizeModel() { size = clothSize });
            }

            foreach (ItemVariantDto itemVariant in dto.Variants)
            {
                foreach (SizeModel s in sizesModel)
                    s.count = 0;

                foreach(SizeModel sizeModel in itemVariant.Stock)
                {
                    sizesModel.FirstOrDefault(s => s.size == sizeModel.size).count = sizeModel.count;
                }

                string stockJson = JsonSerializer.Serialize(sizesModel);

                ItemStock item = new ItemStock()
                {
                    Item = newItem,
                    Price = itemVariant.Price,
                    Discount = itemVariant.Discount,
                    Color = itemVariant.Color,
                    Stock = stockJson
                };
                itemsStock.Add(item);
            }
            
            List<ItemCategory> itemCategory = dto.CategoriesId.Select(ic => new ItemCategory()
            {
                Item = newItem,
                CategoryId = ic
            }).ToList();

            db.Items.Add(newItem);
            db.ItemsStock.AddRange(itemsStock);
            db.ItemsCategories.AddRange(itemCategory);
            db.SaveChanges();
            return true;
        }
    }
}
