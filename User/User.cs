using System;
using System.Net;
using System.Net.Http;

namespace User
{
    public class User
    {
        public HttpClientHandler Handler { get; private set; }

        public string Login { get; private set; }
        public string Password { get; private set; }

        public User(string login, string password)
        {
            Initialization(login, password);
        }

        public User(string login, string password, WebProxy proxy)
        {
            Handler = new HttpClientHandler {Proxy = proxy};
            Initialization(login, password);
        }

        public override bool Equals(object? obj)
        {
            var user = (User) obj;
            if (user == null)
            {
                throw new ArgumentException("Obj can't be equal to null");
            }
            if (Login == user.Login && Password == user.Password && Handler.Equals(user.Handler))
            {
                return true;
            }

            return false;
        }

        private void Initialization(string login, string password)
        {
            if (Handler == null) Handler = new HttpClientHandler();

            CookieContainer container = new CookieContainer();
            Handler.UseCookies = true;
            Handler.CookieContainer = container;
            Login = login;
            Password = password;
        }
    }
}