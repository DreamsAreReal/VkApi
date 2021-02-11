using System.Threading.Tasks;

namespace Authorization
{
    public interface IAuthorization
    {
        Task Login();
    }
}