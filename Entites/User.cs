using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopApp.Entites
{
    public class User
    {
        public int Id { get; set; }
        [Column(TypeName = "varchar(200)")]
        public string Name { get; set; }
        [Column(TypeName = "varchar(200)")]
        public string Surname { get; set; }
        [Column(TypeName = "varchar(200)")]
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreationDate { get; set; }
        public string Salt { get; set; }
        public virtual ICollection<Address> Addresses { get; set;}
        public virtual Cart Cart { get; set; }
        public int CartId { get; set; }
        public ICollection<Session> Sessions { get; set; }
    }
}
