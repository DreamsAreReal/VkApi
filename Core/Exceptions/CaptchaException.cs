using System;
using System.Runtime.Serialization;

namespace Core.Exceptions
{
    [Serializable]
    public class CaptchaException : Exception
    {
        public string Login
        {
            get => _login;
            set => _login = value;
        }

        public string Password
        {
            get => _password;
            set => _password = value;
        }

        private string _login;
        private string _password;

        public CaptchaException()
        {
        }

        public CaptchaException(string message) : base(message)
        {
        }

        public CaptchaException(string message, Exception inner) : base(message, inner)
        {
        }

        protected CaptchaException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}