using ShopApp.Models;

namespace ShopApp.Interfaces
{
    public interface IAccountService
    {
        bool Register(RegisterDto registerDto);
        Guid Login(LoginDto loginDto);

    }
}
