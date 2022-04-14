using System;
using System.Runtime.Serialization;

namespace PL
{
    [Serializable]
    public class EmptyException : Exception
    {
        public EmptyException() { }
        public EmptyException(string message) : base(message) { }
        public EmptyException(string message, Exception innerException) : base(message, innerException) { }
        public EmptyException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}