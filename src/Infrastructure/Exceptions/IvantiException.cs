using System;
using System.Runtime.Serialization;

namespace Infrastructure.Exceptions
{
    [Serializable]
    public class IvantiException : InfrastructureException
    {
        public IvantiException()
        {
        }

        public IvantiException(string? message)
            : base(message)
        {
        }

        public IvantiException(string? message, Exception? innerException)
            : base(message, innerException)
        {
        }

        protected IvantiException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
