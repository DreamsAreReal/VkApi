using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Authorization.Exceptions;
using Core.Exceptions;

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
            string loginUrl = await GetLoginUrl(user);
            Dictionary<string, string> queryData = new Dictionary<string, string>
            {
                {"email", user.Login},
                {"pass", user.Password}
            };
            string userPage = "";
            using (HttpClient client = new HttpClient(user.Handler))
            {
                userPage = await (await client.PostAsync(loginUrl, new FormUrlEncodedContent(queryData))).Content
                        .ReadAsStringAsync();

            }

            CheckAuthorization(userPage, user);
        }

        private void CheckAuthorization(string page, User.User user)
        {
            if (String.IsNullOrEmpty(page))
            {
                throw new NullReferenceException("Answer from login is empty");
            }

            string banString = new Regex("<div class=\"text_panel login_blocked_panel\">\\W+(.*)<a")
                .Match(page).Groups[1].Value;
            if (!String.IsNullOrEmpty(banString))
            {
                var exception = new BanAccountException(banString) {Login = user.Login, Password = user.Password};
                throw exception;
            }

            if (new Regex("service_msg_warning").IsMatch(page))
            {
                var exception = new InvalidDataException {Login = user.Login, Password = user.Password};
                throw exception;
            }

            if (new Regex("login_blocked_panel").IsMatch(page))
            {
                var exception = new CodeNeededException("We need code on login page.")
                    {Login = user.Login, Password = user.Password};
                throw exception;
            }

            if (new Regex("captcha_key").IsMatch(page))
            {
                var exception = new CaptchaException("We need to resolve captcha on login page.")
                    {Login = user.Login, Password = user.Password};
                throw exception;
            }

            if (!new Regex("new_post_placeholder").IsMatch(page))
            {
                throw new UnknownException();
            }
        }

        private async Task<string> GetLoginUrl(User.User user)
        {
            string loginUrl = "";

            using (HttpClient client = new HttpClient(user.Handler))
            {
                string loginPageHtml = await (await client.GetAsync(_url + "/login")).Content.ReadAsStringAsync();

                loginUrl = new Regex("<form method=\"POST\" action=\"(.*)\" novalidate>")
                    .Match(loginPageHtml).Groups[1].Value;
            }

            if (String.IsNullOrEmpty(loginUrl))
            {
                throw new NullReferenceException("Login url is empty!");
            }

            return loginUrl;
        }
    }
}