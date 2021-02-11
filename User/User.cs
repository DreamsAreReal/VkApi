using System;
using System.Net;
using System.Net.Http;

namespace User
{
    public class User
    {
        public HttpClientHandler Handler
        {
            get
            {
                HttpClientHandler handler = new HttpClientHandler
                {
                    UseCookies = true,
                    CookieContainer = _cookie,
                };

                if (_proxy != null)
                {
                    handler.UseProxy = true;
                    handler.Proxy = _proxy;
                }


                return handler;
            }
        }

        private readonly IWebProxy _proxy;
        private CookieContainer _cookie;


        public string Login { get; private set; }
        public string Password { get; private set; }

        public User(string login, string password)
        {
            Initialization(login, password);
        }

        public User(string login, string password, WebProxy proxy)
        {
            _proxy = proxy;
            Initialization(login, password);
        }

        public override bool Equals(object? obj)
        {
            var user = (User) obj;
            if (user == null)
            {
                throw new ArgumentException("Obj can't be equal to null");
            }

            if (Login == user.Login
                && Password == user.Password
                && _cookie.Equals(user._cookie)
                && _proxy.Equals(user._proxy))
            {
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            return $"Login: {Login}. Password: {Password}";
        }

        private void Initialization(string login, string password)
        {
            _cookie = new CookieContainer();
            Login = login;
            Password = password;
        }
    }
}