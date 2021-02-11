namespace Core
{
    public abstract class AbstractService
    {
        protected User _user;

        public AbstractService(User user)
        {
            _user = user;
        }
    }
}