using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Core;
using Core.Exceptions;

namespace User
{
    public class MessagesService : AbstractService
    {
        public MessagesService(Core.User user) : base(user)
        {
        }


        public async Task<long> Send(long peerId, string message)
        {
            if (String.IsNullOrEmpty(message))
            {
                throw new ArgumentException("Message can't be null or empty!");
            }

            string messagesPage = await (await Client.GetAsync(Url + $"/write{peerId.ToString()}"))
                .Content.ReadAsStringAsync();
            string urlToSend =
                new Regex(
                        "<form id=\"write_form\" action=\"(.*)\" method=\"post\">")
                    .Match(messagesPage).Groups[1].Value;

            Dictionary<string, string> queryParams = new Dictionary<string, string>
            {
                {"message", message},
                {"random_id", new Random().Next().ToString()},
                {"entrypoint", ""},
                {"_ajax", "1"}
            };

            if (String.IsNullOrEmpty(urlToSend))
            {
                // Todo: need to check auth, or captcha
                throw new UnknownException();
            }

            string postQueryAnswer = await (await Client.PostAsync(Url + urlToSend, new FormUrlEncodedContent(queryParams)))
                .Content.ReadAsStringAsync();

            return GetLastMessageId(postQueryAnswer);
        }

        public async Task DeleteMessageForAll(long peerId, long messageId)
        {


            string messagesPage = await (await Client.GetAsync(Url + $"/write{peerId.ToString()}"))
                .Content.ReadAsStringAsync();
            string urlToSend =
                new Regex(
                        "<form id=\"write_form\" action=\"(.*)\" method=\"post\">")
                    .Match(messagesPage).Groups[1].Value;
            string hash = new Regex("&hash=(.*)&_af").Match(urlToSend).Groups[1].Value;

            Dictionary<string, string> queryParams = new Dictionary<string, string>
            {
                {"act", "delete_for_all"},
                {"peer", peerId.ToString()},
                {"ids", messageId.ToString()},
                {"hash", hash},
                {"_ajax", "1"}
            };


            string postQueryAnswer = await (await Client.PostAsync(Url + "/mail", new FormUrlEncodedContent(queryParams)))
                .Content.ReadAsStringAsync();
            return;

        }



        private long GetLastMessageId(string page)
        {
            string lastId = new Regex("msg_item\\W+_msg(.*)\"").Match(page).Groups[1].Value;
            if (!long.TryParse(lastId, out var id))
            {
                // Todo: normal exception
                // Todo: create model and return this
                // Todo: equal name, text and return
                throw new Exception();
            }
            return id;
        }
    }
}