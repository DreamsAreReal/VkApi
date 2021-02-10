using System;
using System.Runtime.Serialization;

namespace Authorization.Exceptions
{
    [Serializable]
    public class InvalidDataException : Exception
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
        public InvalidDataException()
        {
        }

        public InvalidDataException(string message) : base(message)
        {
        }

        public InvalidDataException(string message, Exception inner) : base(message, inner)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }

        protected InvalidDataException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }

}