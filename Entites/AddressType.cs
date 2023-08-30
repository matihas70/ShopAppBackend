using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopApp.Entites
{
    public class AddressType
    {
        public int Id { get; set; }
        [Column(TypeName = "varchar(200)")]
        public string Name { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
    }
}
