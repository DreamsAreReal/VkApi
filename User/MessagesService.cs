using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Core;

namespace User
{
    public class MessagesService : AbstractService
    {
        public MessagesService(Core.User user) : base(user)
        {
        }

        public async Task<long> SendToUser(long id, string message)
        {
            if (String.IsNullOrEmpty(message))
            {
                throw new ArgumentException("Message can't be null or empty!");
            }


            Client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (iPhone; CPU iPhone OS 13_2_3 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/13.0.3 Mobile/15E148 Safari/604.1");
            Client.DefaultRequestHeaders.Add("Referer", "https://m.vk.com/");
            string messagesPage = await (await Client.GetAsync(Url + $"/write{id.ToString()}"))
                .Content.ReadAsStringAsync();
            string urlToSend =
                new Regex(
                        "<form class=\"uMailWrite uMailWrite_canAttachMoney uMailWrite_canAttachPoll\" method=\"POST\" action=\"(.*)\" onsubmit=")
                    .Match(messagesPage).Groups[1].Value;


            return 0;
        }
    }
}