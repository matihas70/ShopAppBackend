using System.ComponentModel;

namespace ShopApp.Entites
{
    public class Cart
    {
        public int Id { get; set; }
        public string Items { get; set; }
        public virtual User User { get; set; }
    }
}
