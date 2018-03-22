using System;
using System.Runtime.Serialization;

namespace BistroFiftyTwo.Server.Parser
{
    internal class RecipeParseException : Exception
    {
        public RecipeParseException()
        {
        }

        public RecipeParseException(string message) : base(message)
        {
        }

        public RecipeParseException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RecipeParseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}