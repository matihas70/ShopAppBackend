using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ShopApp.Entites
{
    public class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions<ShopContext> options) : base(options)
        {
        
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<AddressType> AddressTypes { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Session> Sessions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(x => x.Cart)
                .WithOne(x => x.User)
                .HasForeignKey<User>(x => x.CartId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Address>()
                .HasOne(x => x.User)
                .WithMany(x => x.Addresses)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<Address>()
                .HasOne(x => x.Type)
                .WithMany(x => x.Addresses)
                .HasForeignKey(x => x.TypeId);

            modelBuilder.Entity<Item>()
                .HasOne(x => x.Category)
                .WithMany(x => x.Items)
                .HasForeignKey(x => x.CategoryId);

            modelBuilder.Entity<Item>()
                .HasOne(x => x.Brand)
                .WithMany(x => x.Items)
                .HasForeignKey(x => x.BrandId);

            modelBuilder.Entity<Category>()
                .HasOne(x => x.SuperCategory)
                .WithMany(x => x.SubCategories)
                .HasForeignKey(x => x.SuperCategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Session>()
                .HasOne(x => x.User)
                .WithMany(x => x.Sessions)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<Cart>()
                .Property(x => x.Items)
                .HasDefaultValue("[]");

            base.OnModelCreating(modelBuilder);
        }
    }
}
