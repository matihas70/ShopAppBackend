namespace ShopApp.Exceptions
{
    public class WrongPasswordException : Exception
    {
        public WrongPasswordException() : base("Incorrect password") 
        {

        }
    }
}
