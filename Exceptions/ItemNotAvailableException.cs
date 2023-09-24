namespace ShopApp.Exceptions
{
    public class ItemNotAvailableException : Exception
    {
        public ItemNotAvailableException() : base("Item not available")
        { 
        
        }
        public ItemNotAvailableException(int quantityLeft) : base($"Quantity not available, only {quantityLeft} pieces left")
        {

        }
    }
}
