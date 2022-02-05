using System.Runtime.Serialization;

namespace Finance.Framework
{
    public class ProjectionException : Exception
    {
        public ProjectionException()
        {
        }

        public ProjectionException(string? message)
            : base(message)
        {
        }

        public ProjectionException(string? message, Exception? innerException)
            : base(message, innerException)
        {
        }
    }
}