using Microsoft.AspNetCore.Mvc;
using ShopApp.Interfaces;
using ShopApp.Models;
using ShopApp.Services;

namespace ShopApp.Controllers
{
    [Controller]
    public class AccountController : Controller
    {
        private readonly IUserAccount userAccount;
        public AccountController(IUserAccount _userAccount)
        {
            userAccount = _userAccount;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login([FromBody]LoginDto loginDto)
        {
            if(userAccount.Login(loginDto))
                return Ok("Logowanie udane");

            return BadRequest("Logowanie nieudane");
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
    }
}
