using ShopApp.Models;

namespace ShopApp.Interfaces
{
    public interface IAccount
    {
        bool Register(RegisterDto registerDto);
        Guid Login(LoginDto loginDto);

    }
}
