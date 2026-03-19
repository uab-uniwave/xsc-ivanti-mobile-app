using System;
using System.Runtime.Serialization;

namespace Infrastructure.Exceptions
{
    [Serializable]
    public abstract class InfrastructureException : Exception
    {
        protected InfrastructureException()
        {
        }

        protected InfrastructureException(string? message)
            : base(message)
        {
        }

        protected InfrastructureException(string? message, Exception? innerException)
            : base(message, innerException)
        {
        }

        protected InfrastructureException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
