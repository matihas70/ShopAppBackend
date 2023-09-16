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
            InputItemsDto dto = new InputItemsDto()
            {
                Gender = (byte)gender,
                CategoriesId = categoriesId,
            };
            List<OutputItemDto> items = itemService.GetItems(dto);
            return Ok(items);
        }

        [HttpGet]
        [Route("Categories")]
        public IActionResult Categories()
        {
            return Ok(itemService.GetCategories());
        }


    }
}
