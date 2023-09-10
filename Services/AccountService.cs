using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Identity.Client;
using ShopApp.Entites;
using ShopApp.Models;
using ShopApp.Interfaces;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Security.Cryptography;

namespace ShopApp.Services
{
    public class AccountService : Interfaces.IAccountService
    {
        private readonly IDbContextFactory<ShopContext> dbContextFactory;
        public AccountService(IDbContextFactory<ShopContext> _dbContextFactory)
        {
            dbContextFactory = _dbContextFactory;
        }

        public bool Register(RegisterDto registerDto)
        {
            using ShopContext db = dbContextFactory.CreateDbContext();

            if (db.Users.FirstOrDefault(u => u.Email == registerDto.Email) != null)
                return false;

            string salt = GenRandomSalt(16);
            byte[] bytes = Encoding.UTF8.GetBytes(salt + registerDto.Password + Consts.Security.pepper);
            byte[] hashedPassword = SHA256.HashData(bytes);

            User user = new User();
            user.Salt = salt;
            user.Password = Encoding.ASCII.GetString(hashedPassword);
            user.Email = registerDto.Email;
            user.CreationDate = DateTime.Now;
            user.Name = registerDto.Name;
            user.Surname = registerDto.Surname;
            
            Cart cart = new Cart();
            cart.User = user;

            db.Users.Add(user);
            db.Carts.Add(cart);
            db.SaveChanges();
            return true;
        }

        
        public Guid Login(LoginDto loginDto)
        {
            using ShopContext db = dbContextFactory.CreateDbContext();
            
            User user = db.Users.FirstOrDefault(u => u.Email == loginDto.Email);
            if (user == null)
                return Guid.Empty;

            byte[] bytes = Encoding.UTF8.GetBytes(user.Salt + loginDto.Password + Consts.Security.pepper);
            byte[] hashedPassword = SHA256.HashData(bytes);

            if(!(user.Password == Encoding.ASCII.GetString(hashedPassword)))
                return Guid.Empty;

            Session session = new Session()
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                CreationDate = DateTime.Now,
                ExpirationDate = DateTime.Now.AddMonths(3)
            };
            
            db.Add(session);
            db.SaveChanges();

            return session.Id;
        }

        private string GenRandomSalt(int lettersCount)
        {
            Random rnd = new Random();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < lettersCount; i++)
            {
                ushort letter = (ushort)rnd.Next(33, 127);
                sb.Append((char)letter);
            }
            string salt = sb.ToString();

            byte[] bytesSalt = Encoding.ASCII.GetBytes(salt);
            string base64Salt = Convert.ToBase64String(bytesSalt);
            return salt;
        }
    }
}
