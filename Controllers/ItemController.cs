using Microsoft.AspNetCore.Mvc;
using ShopApp.Interfaces;

namespace ShopApp.Controllers
{
    [ApiController]
    public class ItemController : Controller
    {
        private readonly IItemService itemService;

        public ItemController(IItemService _itemService) 
        {
            itemService = _itemService;
        }

        [HttpGet]
        public IActionResult Categories()
        {
            return Ok(itemService.GetCategories());
        }
    }
}
