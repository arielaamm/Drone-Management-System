using System;
using System.Runtime.Serialization;

namespace PL
{
    /// <summary>
    /// Defines the <see cref="EmptyException" />.
    /// </summary>
    [Serializable]
    public class EmptyException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmptyException"/> class.
        /// </summary>
        public EmptyException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmptyException"/> class.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        public EmptyException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmptyException"/> class.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        /// <param name="innerException">The innerException<see cref="Exception"/>.</param>
        public EmptyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmptyException"/> class.
        /// </summary>
        /// <param name="info">The info<see cref="SerializationInfo"/>.</param>
        /// <param name="context">The context<see cref="StreamingContext"/>.</param>
        public EmptyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
