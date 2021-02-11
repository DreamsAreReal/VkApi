using System;
using System.Runtime.Serialization;

namespace Authorization.Exceptions
{
    [Serializable]
    public class CodeNeededException : Exception
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

        public CodeNeededException()
        {
        }

        public CodeNeededException(string message) : base(message)
        {
        }

        public CodeNeededException(string message, Exception inner) : base(message, inner)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue("Login", _login);
            info.AddValue("Password", _password);
        }

        protected CodeNeededException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}