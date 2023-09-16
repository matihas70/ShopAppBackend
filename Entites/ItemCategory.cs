using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopApp.Entites
{
    public class ItemCategory
    {
        public int ItemId { get; set; }
        public int CategoryId { get; set; }
    }
}
