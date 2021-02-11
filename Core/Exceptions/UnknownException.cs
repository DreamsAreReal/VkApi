using System;
using System.Runtime.Serialization;

namespace Core.Exceptions
{
    [Serializable]
    public class UnknownException : Exception
    {
        public UnknownException()
        {
        }

        public UnknownException(string message) : base(message)
        {
        }

        public UnknownException(string message, Exception inner) : base(message, inner)
        {
        }

        protected UnknownException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}