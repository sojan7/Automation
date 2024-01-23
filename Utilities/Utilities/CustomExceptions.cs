using System.Runtime.Serialization;

namespace Utilities.Utilities
{
    public class CustomExceptions : Exception
    {
        public CustomExceptions() : base()
        {
        }

        public CustomExceptions(string message) : base(message)
        {
        }

        public CustomExceptions(string message, Exception innerException) : base(message, innerException)
        {
        }

        public CustomExceptions(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}