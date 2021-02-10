namespace Authorization
{
    public interface IAuthorization
    {
        bool Login(User.User user);
    }
}