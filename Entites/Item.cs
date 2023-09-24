using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopApp.Entites
{
    public partial class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public virtual Brand Brand { get; set; }
        public int BrandId { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public byte Gender { get; set; }
        public string? Sizes { get; set; }
        public DateTime AddDate { get; set; } = DateTime.Now;
        public string? Pictures { get; set; }
        public virtual List<ItemStock> ItemsStock { get; set; }
    }
}
