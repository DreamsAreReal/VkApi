using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Authorization.Exceptions;
using Core;
using Core.Exceptions;

namespace Authorization
{
    public class BasicAuthorization : AbstractService, IAuthorization
    {

        public BasicAuthorization(User user) : base(user)
        {
        }

        public async Task Login()
        {
            string loginUrl = await GetLoginUrl();
            Dictionary<string, string> queryData = new Dictionary<string, string>
            {
                {"email", User.Login},
                {"pass", User.Password}
            };

            string userPage = await (await Client.PostAsync(loginUrl, new FormUrlEncodedContent(queryData))).Content
                .ReadAsStringAsync();



            CheckAuthorization(userPage);
        }

        private void CheckAuthorization(string page)
        {
            if (String.IsNullOrEmpty(page))
            {
                throw new NullReferenceException("Answer from login is empty");
            }

            string banString = new Regex("<div class=\"text_panel login_blocked_panel\">\\W+(.*)<a")
                .Match(page).Groups[1].Value;
            if (!String.IsNullOrEmpty(banString))
            {
                var exception = new BanAccountException(banString) {Login = User.Login, Password = User.Password};
                throw exception;
            }

            if (new Regex("service_msg_warning").IsMatch(page))
            {
                var exception = new InvalidDataException {Login = User.Login, Password = User.Password};
                throw exception;
            }

            if (new Regex("login_blocked_panel").IsMatch(page))
            {
                var exception = new CodeNeededException("We need code on login page.")
                    {Login = User.Login, Password = User.Password};
                throw exception;
            }

            if (new Regex("captcha_key").IsMatch(page))
            {
                var exception = new CaptchaException("We need to resolve captcha on login page.")
                    {Login = User.Login, Password = User.Password};
                throw exception;
            }

            if (!new Regex("new_post_placeholder").IsMatch(page))
            {
                throw new UnknownException();
            }
        }

        private async Task<string> GetLoginUrl()
        {


            string loginPageHtml = await (await Client.GetAsync(Url + "/login")).Content.ReadAsStringAsync();

            string loginUrl = new Regex("<form method=\"POST\" action=\"(.*)\" novalidate>")
                    .Match(loginPageHtml).Groups[1].Value;


            if (String.IsNullOrEmpty(loginUrl))
            {
                throw new NullReferenceException("Login url is empty!");
            }

            return loginUrl;
        }


    }
}