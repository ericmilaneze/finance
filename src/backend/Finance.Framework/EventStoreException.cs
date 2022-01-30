using System.Runtime.Serialization;

namespace Finance.Framework
{
    public class EventStoreException : Exception
    {
        public EventStoreException()
        {
        }

        public EventStoreException(string? message)
            : base(message)
        {
        }

        public EventStoreException(string? message, Exception? innerException)
            : base(message, innerException)
        {
        }
    }
}