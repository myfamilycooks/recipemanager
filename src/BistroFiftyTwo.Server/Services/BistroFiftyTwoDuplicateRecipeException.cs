using System;
using System.Runtime.Serialization;

namespace BistroFiftyTwo.Server.Services
{
    public class BistroFiftyTwoDuplicateRecipeException : Exception
    {
        public BistroFiftyTwoDuplicateRecipeException()
        {
        }

        public BistroFiftyTwoDuplicateRecipeException(string message) : base(message)
        {
        }

        public BistroFiftyTwoDuplicateRecipeException(string message, Exception innerException) : base(message,
            innerException)
        {
        }

        protected BistroFiftyTwoDuplicateRecipeException(SerializationInfo info, StreamingContext context) : base(info,
            context)
        {
        }
    }
}