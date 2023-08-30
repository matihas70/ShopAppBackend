using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopApp.Entites
{
    public class Address
    {
        public int Id { get; set; }
        [Column(TypeName = "varchar(200)")]
        public string Country { get; set; }

        public string City { get; set; }
        public string Street { get; set; }
        [Column(TypeName = "varchar(10)")]
        public string PostalCode { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int TypeId { get; set; }
        public virtual AddressType Type { get; set; }
    }
}
