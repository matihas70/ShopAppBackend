namespace ShopApp.Exceptions
{
    public class ItemNotFoundException : RessourceNotFoundException
    {
        public ItemNotFoundException() : base("Item not found") 
        {
        
        }
    }
}
