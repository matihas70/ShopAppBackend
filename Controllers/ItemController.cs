using Microsoft.AspNetCore.Mvc;
using ShopApp.Enums;
using ShopApp.Interfaces;
using ShopApp.Models;

namespace ShopApp.Controllers
{
    [Route("Items")]
    [ApiController]
    public class ItemsController : Controller
    {
        private readonly IItemService itemService;

        public ItemsController(IItemService _itemService)
        {
            itemService = _itemService;
        }

        [HttpGet("{catType?}")]
        public IActionResult getItems([FromRoute]string? catType, [FromQuery(Name = "gender")] int gender, [FromQuery] List<int> categoriesId)
        {
            InputGetItemsDto dto = new InputGetItemsDto()
            {
                Gender = (GenderEnum)gender,
                CategoriesId = categoriesId,
            };
            List<OutputGetItemDto> items = itemService.GetItems(dto);
            return Ok(items);
        }

        [HttpGet]
        [Route("Categories")]
        public IActionResult Categories()
        {
            return Ok(itemService.GetCategories());
        }
        [HttpPost]
        [Route("AddItem")]
        public IActionResult AddItem([FromBody] AddItemDto dto)
        {
            if (itemService.AddItem(dto))
            {
                return Ok("Item added");
            }
            return BadRequest();
        }


    }
}
