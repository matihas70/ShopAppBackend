using Microsoft.AspNetCore.Mvc;
using ShopApp.Interfaces;
using ShopApp.Models;
using ShopApp.Services;
using System.Net;

namespace ShopApp.Controllers
{
    [Controller]
    public class AccountController : Controller
    {
        private readonly IAccountService userAccount;
        public AccountController(IAccountService _userAccount)
        {
            userAccount = _userAccount;
        }

        [HttpPost]
        //[Route("login")]
        public IActionResult Login([FromBody]LoginDto loginDto)
        {
            Guid guid = userAccount.Login(loginDto);
            
            Response.Cookies.Append("Id", guid.ToString(), new CookieOptions() {Secure = false, HttpOnly = true});
            return Ok("Successfull login");
            
        }

        [HttpPost]
        public IActionResult Register([FromBody]RegisterDto registerDto)
        {
            if (userAccount.Register(registerDto))
            {  
                return Ok("Successful register");
            }
            
            return BadRequest();
        }

        public IActionResult Logout()
        {
            Response.Cookies.Delete("Id");
            return Ok();
        }
    }
}
