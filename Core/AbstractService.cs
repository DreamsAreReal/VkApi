namespace Core
{
    public abstract class AbstractService
    {
        protected User User;
        protected const string Url = "https://m.vk.com/";

        public AbstractService(User user)
        {
            User = user;
        }
    }
}