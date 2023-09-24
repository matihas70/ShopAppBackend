namespace ShopApp.Exceptions
{
    public class UserNotFoundException : RessourceNotFoundException
    {
        public UserNotFoundException() : base("User not found")
        {
        
        }
    }
}
