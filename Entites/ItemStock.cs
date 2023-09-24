using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopApp.Entites
{
    public class ItemStock
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        [Precision(18, 2)]
        public decimal Price { get; set; }
        [Column("Discount[%]")]
        public int Discount { get; set; } //%
        public Item Item { get; set; }
        public string Color { get; set; }
        public string Stock { get; set; }
        public DateTime AddDate { get; set; } = DateTime.Now;
    }
}
