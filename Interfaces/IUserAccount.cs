using ShopApp.Models;

namespace ShopApp.Interfaces
{
    public interface IUserAccount
    {
        bool Register(RegisterDto registerDto);
        bool Login(LoginDto loginDto);
    }
}
