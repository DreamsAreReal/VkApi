using System;
using System.Runtime.Serialization;

namespace Authorization.Exceptions
{
    [Serializable]
    public class BanAccountException : Exception, ISerializable
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

        public BanAccountException()
        {
        }

        public BanAccountException(string message) : base(message)
        {
        }

        public BanAccountException(string message, Exception inner) : base(message, inner)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue("Login", _login);
            info.AddValue("Password", _password);
        }

        protected BanAccountException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }

    }
}