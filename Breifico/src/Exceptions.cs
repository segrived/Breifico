using System;
using System.Runtime.Serialization;

namespace Breifico
{
    [Serializable]
    public class InvalidBmpImageException : Exception
    {
        public InvalidBmpImageException() {}
        public InvalidBmpImageException(string message) : base(message) {}
        public InvalidBmpImageException(string message, Exception inner) : base(message, inner) {}

        protected InvalidBmpImageException(
            SerializationInfo info,
            StreamingContext context) : base(info, context) {}
    }
}
