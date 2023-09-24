using Microsoft.AspNetCore.Mvc;
using ShopApp.Enums;
using ShopApp.Interfaces;
using ShopApp.Models;

namespace ShopApp.Controllers
{
    [ApiController]
    [Route("Items")]
    public class ItemController : Controller
    {
        private readonly IItemService itemService;

        public ItemController(IItemService _itemService)
        {
            itemService = _itemService;
        }

        [HttpGet]
        [Route("GetItems")]
        public IActionResult Items([FromQuery]GenderEnum gender, [FromQuery]List<int> categoriesId)
        {
            InputGetItemsDto dto = new InputGetItemsDto()
            {
                Gender = (byte)gender,
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
