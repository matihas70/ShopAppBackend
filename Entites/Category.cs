using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopApp.Entites
{
    public partial class Category
    {
        public int Id { get; set; }
        [Column(TypeName = "varchar(200)")]
        public string Name { get; set; }
        public virtual ICollection<Category> SubCategories { get; set; }
        public virtual Category SuperCategory { get; set; }
        public int? SuperCategoryId { get; set; }
        public virtual ICollection<Item> Items { get; set; }
    }
}
