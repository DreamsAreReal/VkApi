using System.Threading.Tasks;

namespace Authorization
{
    public interface IAuthorization
    {
        Task Login(Core.User user);
    }
}