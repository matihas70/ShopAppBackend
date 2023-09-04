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
        private readonly IAccount userAccount;
        public AccountController(IAccount _userAccount)
        {
            userAccount = _userAccount;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        //[Route("login")]
        public IActionResult Login([FromBody]LoginDto loginDto)
        {
            Guid guid = userAccount.Login(loginDto);
            if (guid == Guid.Empty)
                return BadRequest("Logowanie nieudane");
            
            Response.Cookies.Append("Id", guid.ToString(), new CookieOptions() {Secure = false, HttpOnly = false});
            Response.Headers.Add("myheader", "myValue");
            return Ok();
            
        }

        [HttpPost]
        public IActionResult Register([FromBody]RegisterDto registerDto)
        {
            if (userAccount.Register(registerDto))
            {  
                return Ok("Rejestracja udana");
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
