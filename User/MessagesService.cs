using System;
using System.Net.Http;
using Core;

namespace User
{
    public class MessagesService : AbstractService
    {
        public MessagesService(Core.User user) : base(user)
        {
        }

        public async void SendToUser(long id, string message)
        {
            if (String.IsNullOrEmpty(message))
            {
                throw new ArgumentException("Message can't be null or empty!");
            }

            using (HttpClient client = new HttpClient(_user.Handler))
            {

            }
        }
    }
}