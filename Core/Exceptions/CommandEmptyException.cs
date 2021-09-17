using System;
using System.Runtime.Serialization;

namespace Core.Exceptions
{
    [Serializable]
    public class CommandEmptyException : Exception
    {
        public CommandEmptyException()
        {
        }

        public CommandEmptyException(string message) : base(message)
        {
        }

        public CommandEmptyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CommandEmptyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}