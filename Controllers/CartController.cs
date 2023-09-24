using Microsoft.AspNetCore.Mvc;
using ShopApp.Enums;
using ShopApp.Interfaces;
using ShopApp.Models;

namespace ShopApp.Controllers
{
    [ApiController]
    [Route("Cart")]
    public class CartController : Controller
    {
        private readonly ICartService cartService;

        public CartController(ICartService _cartService)
        {
            cartService = _cartService;
        }

        [HttpPut]
        [Route("Add")]
        public IActionResult AddToCart([FromBody] AddToCartDto dto)
        {
            string sessionId = Request.Cookies.FirstOrDefault(x => x.Key == "Id").Value;

            Guid.TryParse(sessionId, out Guid guid);
            
            cartService.AddToCart(dto, guid);

            return Ok("Added");
        }
        [HttpPut]
        [Route("Update")]
        public IActionResult UpdateItemQuantity([FromBody] UpdateItemQuantityDto dto)
        {
            string sessionId = Request.Cookies.FirstOrDefault(x => x.Key == "Id").Value;
            Guid.TryParse(sessionId, out Guid guid);
            cartService.UpdateItemQuantity(dto, guid);
            return Ok("Updated");
        }
    }
}
