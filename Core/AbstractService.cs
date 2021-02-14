using System;
using System.Net.Http;

namespace Core
{
    public abstract class AbstractService : IDisposable
    {
        protected User User;
        protected const string Url = "https://m.vk.com";
        protected HttpClient Client;

        public AbstractService()
        {
            InitializationClient();
        }

        public AbstractService(User user)
        {
            User = user;
            Client = new HttpClient(User.Handler);
            InitializationClient();
        }

        public void Dispose()
        {
            Client?.Dispose();
        }

        private void InitializationClient()
        {
            if (Client == null) Client = new HttpClient();
            // TODO: Generate user-agent
            Client.DefaultRequestHeaders.Add("User-Agent",
                "Mozilla/5.0 (iPhone; CPU iPhone OS 13_2_3 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/13.0.3 Mobile/15E148 Safari/604.1");
        }




    }
}