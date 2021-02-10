using System;
using System.Net;

namespace User
{
    public class User
    {
        public CookieContainer CookieContainer { get; private set; }

        public string Login { get; }
        public string Password { get; }

        public User(string login, string password)
        {
            CookieContainer = new CookieContainer();
            Login = login;
            Password = password;
        }

        public override bool Equals(object? obj)
        {
            var user = (User) obj;
            if (user == null)
            {
                throw new ArgumentException("Obj can't be equal to null");
            }
            if (Login == user.Login && Password == user.Password && CookieContainer.Equals(user.CookieContainer))
            {
                return true;
            }

            return false;
        }
    }
}