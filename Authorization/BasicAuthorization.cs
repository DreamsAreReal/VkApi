namespace Authorization
{
    public class BasicAuthorization : IAuthorization
    {
        public bool Login(User.User user)
        {
            return false;
        }
    }
}