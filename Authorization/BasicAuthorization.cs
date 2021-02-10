using System.Net.Http;
using System.Threading.Tasks;

namespace Authorization
{
    public class BasicAuthorization : IAuthorization
    {
        private string _url;

        public BasicAuthorization()
        {
            _url = Core.Settings.Url;
        }

        public async Task Login(User.User user)
        {

            using (HttpClient client = new HttpClient(user.Handler))
            {
                var query = await client.GetAsync(_url);

            }

        }
    }
}