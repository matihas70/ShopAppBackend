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
        public DbSet<ItemStock> ItemsStock { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ItemCategory> ItemsCategories { get; set; }
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

            modelBuilder.Entity<Address>(address =>
            {
                address.HasOne(x => x.User)
                       .WithMany(x => x.Addresses)
                       .HasForeignKey(x => x.UserId);

                address.HasOne(x => x.Type)
                       .WithMany(x => x.Addresses)
                       .HasForeignKey(x => x.TypeId);
            });

            modelBuilder.Entity<Item>(item =>
            {
                item.HasMany(x => x.Categories)
                .WithMany(x => x.Items)
                .UsingEntity<ItemCategory>(
                i => i.HasOne(ic => ic.Category).WithMany().HasForeignKey(ic => ic.CategoryId),
                i => i.HasOne(ic => ic.Item).WithMany().HasForeignKey(ic => ic.ItemId),
                i => i.HasKey(ic => new { ic.ItemId, ic.CategoryId })
                );

                item.HasOne(x => x.Brand)
                .WithMany(x => x.Items)
                .HasForeignKey(x => x.BrandId);
            });

            modelBuilder.Entity<ItemStock>(itemStock =>
            {
                itemStock.HasOne(x => x.Item)
                .WithMany(x => x.ItemsStock)
                .HasForeignKey(x => x.ItemId);
            });


            modelBuilder.Entity<Category>(category =>
            {
                category.HasOne(x => x.SuperCategory)
                        .WithMany(x => x.SubCategories)
                        .HasForeignKey(x => x.SuperCategoryId)
                        .OnDelete(DeleteBehavior.NoAction);

                category.Property(x => x.SuperCategoryId)
                        .IsRequired(false);
            });
                
            modelBuilder.Entity<Session>()
                .HasOne(x => x.User)
                .WithMany(x => x.Sessions)
                .HasForeignKey(x => x.UserId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
