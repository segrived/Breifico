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
        protected InvalidBmpImageException(SerializationInfo info, StreamingContext context) : base(info, context) {}
    }

    [Serializable]
    public class DifferentSizeException : Exception
    {
        public DifferentSizeException() {}
        public DifferentSizeException(string message) : base(message) {}
        public DifferentSizeException(string message, Exception innerException) : base(message, innerException) {}
        protected DifferentSizeException(SerializationInfo info, StreamingContext context) : base(info, context) {}
    }
}