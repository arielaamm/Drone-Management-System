using System;
using System.Runtime.Serialization;

namespace DALExceptionscs
{
    /// <summary>
    /// Defines the <see cref="AlreadyExistException" />.
    /// </summary>
    [Serializable]
    public class AlreadyExistException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AlreadyExistException"/> class.
        /// </summary>
        public AlreadyExistException() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AlreadyExistException"/> class.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        public AlreadyExistException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AlreadyExistException"/> class.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        /// <param name="innerException">The innerException<see cref="Exception"/>.</param>
        public AlreadyExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AlreadyExistException"/> class.
        /// </summary>
        /// <param name="info">The info<see cref="SerializationInfo"/>.</param>
        /// <param name="context">The context<see cref="StreamingContext"/>.</param>
        public AlreadyExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    /// <summary>
    /// Defines the <see cref="DoesNotExistException" />.
    /// </summary>
    [Serializable]
    public class DoesNotExistException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DoesNotExistException"/> class.
        /// </summary>
        public DoesNotExistException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DoesNotExistException"/> class.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        public DoesNotExistException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DoesNotExistException"/> class.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        /// <param name="innerException">The innerException<see cref="Exception"/>.</param>
        public DoesNotExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DoesNotExistException"/> class.
        /// </summary>
        /// <param name="info">The info<see cref="SerializationInfo"/>.</param>
        /// <param name="context">The context<see cref="StreamingContext"/>.</param>
        public DoesNotExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    /// <summary>
    /// Defines the <see cref="DroneInMiddleActionException" />.
    /// </summary>
    [Serializable]
    public class DroneInMiddleActionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DroneInMiddleActionException"/> class.
        /// </summary>
        public DroneInMiddleActionException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DroneInMiddleActionException"/> class.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        public DroneInMiddleActionException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DroneInMiddleActionException"/> class.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        /// <param name="innerException">The innerException<see cref="Exception"/>.</param>
        public DroneInMiddleActionException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DroneInMiddleActionException"/> class.
        /// </summary>
        /// <param name="info">The info<see cref="SerializationInfo"/>.</param>
        /// <param name="context">The context<see cref="StreamingContext"/>.</param>
        public DroneInMiddleActionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    /// <summary>
    /// Defines the <see cref="DroneNotChargingException" />.
    /// </summary>
    [Serializable]
    public class DroneNotChargingException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DroneNotChargingException"/> class.
        /// </summary>
        public DroneNotChargingException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DroneNotChargingException"/> class.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        public DroneNotChargingException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DroneNotChargingException"/> class.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        /// <param name="innerException">The innerException<see cref="Exception"/>.</param>
        public DroneNotChargingException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DroneNotChargingException"/> class.
        /// </summary>
        /// <param name="info">The info<see cref="SerializationInfo"/>.</param>
        /// <param name="context">The context<see cref="StreamingContext"/>.</param>
        public DroneNotChargingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    /// <summary>
    /// Defines the <see cref="ThereAreNoRoomException" />.
    /// </summary>
    [Serializable]
    public class ThereAreNoRoomException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ThereAreNoRoomException"/> class.
        /// </summary>
        public ThereAreNoRoomException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ThereAreNoRoomException"/> class.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        public ThereAreNoRoomException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ThereAreNoRoomException"/> class.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        /// <param name="innerException">The innerException<see cref="Exception"/>.</param>
        public ThereAreNoRoomException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ThereAreNoRoomException"/> class.
        /// </summary>
        /// <param name="info">The info<see cref="SerializationInfo"/>.</param>
        /// <param name="context">The context<see cref="StreamingContext"/>.</param>
        public ThereAreNoRoomException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    /// <summary>
    /// Defines the <see cref="PhoneIsUsedException" />.
    /// </summary>
    [Serializable]
    public class PhoneIsUsedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PhoneIsUsedException"/> class.
        /// </summary>
        public PhoneIsUsedException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PhoneIsUsedException"/> class.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        public PhoneIsUsedException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PhoneIsUsedException"/> class.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        /// <param name="innerException">The innerException<see cref="Exception"/>.</param>
        public PhoneIsUsedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PhoneIsUsedException"/> class.
        /// </summary>
        /// <param name="info">The info<see cref="SerializationInfo"/>.</param>
        /// <param name="context">The context<see cref="StreamingContext"/>.</param>
        public PhoneIsUsedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    /// <summary>
    /// Defines the <see cref="NameIsUsedException" />.
    /// </summary>
    [Serializable]
    public class NameIsUsedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NameIsUsedException"/> class.
        /// </summary>
        public NameIsUsedException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NameIsUsedException"/> class.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        public NameIsUsedException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NameIsUsedException"/> class.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        /// <param name="innerException">The innerException<see cref="Exception"/>.</param>
        public NameIsUsedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NameIsUsedException"/> class.
        /// </summary>
        /// <param name="info">The info<see cref="SerializationInfo"/>.</param>
        /// <param name="context">The context<see cref="StreamingContext"/>.</param>
        public NameIsUsedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    /// <summary>
    /// Defines the <see cref="DeleteException" />.
    /// </summary>
    [Serializable]
    public class DeleteException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteException"/> class.
        /// </summary>
        public DeleteException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteException"/> class.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        public DeleteException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteException"/> class.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        /// <param name="innerException">The innerException<see cref="Exception"/>.</param>
        public DeleteException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteException"/> class.
        /// </summary>
        /// <param name="info">The info<see cref="SerializationInfo"/>.</param>
        /// <param name="context">The context<see cref="StreamingContext"/>.</param>
        public DeleteException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    /// <summary>
    /// Defines the <see cref="XmlLoadException" />.
    /// </summary>
    [Serializable]
    internal class XmlLoadException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="XmlLoadException"/> class.
        /// </summary>
        public XmlLoadException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlLoadException"/> class.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        public XmlLoadException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlLoadException"/> class.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        /// <param name="innerException">The innerException<see cref="Exception"/>.</param>
        public XmlLoadException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlLoadException"/> class.
        /// </summary>
        /// <param name="info">The info<see cref="SerializationInfo"/>.</param>
        /// <param name="context">The context<see cref="StreamingContext"/>.</param>
        protected XmlLoadException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    /// <summary>
    /// Defines the <see cref="XmlWriteException" />.
    /// </summary>
    [Serializable]
    internal class XmlWriteException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="XmlWriteException"/> class.
        /// </summary>
        public XmlWriteException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlWriteException"/> class.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        public XmlWriteException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlWriteException"/> class.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        /// <param name="innerException">The innerException<see cref="Exception"/>.</param>
        public XmlWriteException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlWriteException"/> class.
        /// </summary>
        /// <param name="info">The info<see cref="SerializationInfo"/>.</param>
        /// <param name="context">The context<see cref="StreamingContext"/>.</param>
        protected XmlWriteException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    /// <summary>
    /// Defines the <see cref="XMLFileLoadCreateException" />.
    /// </summary>
    [Serializable]
    public class XMLFileLoadCreateException : Exception
    {
        /// <summary>
        /// Defines the filePath.
        /// </summary>
        private readonly string filePath;

        /// <summary>
        /// Defines the v.
        /// </summary>
        private readonly string v;

        /// <summary>
        /// Defines the ex.
        /// </summary>
        private readonly Exception ex;

        /// <summary>
        /// Initializes a new instance of the <see cref="XMLFileLoadCreateException"/> class.
        /// </summary>
        public XMLFileLoadCreateException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XMLFileLoadCreateException"/> class.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        public XMLFileLoadCreateException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XMLFileLoadCreateException"/> class.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        /// <param name="innerException">The innerException<see cref="Exception"/>.</param>
        public XMLFileLoadCreateException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XMLFileLoadCreateException"/> class.
        /// </summary>
        /// <param name="filePath">The filePath<see cref="string"/>.</param>
        /// <param name="v">The v<see cref="string"/>.</param>
        /// <param name="ex">The ex<see cref="Exception"/>.</param>
        public XMLFileLoadCreateException(string filePath, string v, Exception ex)
        {
            this.filePath = filePath;
            this.v = v;
            this.ex = ex;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XMLFileLoadCreateException"/> class.
        /// </summary>
        /// <param name="info">The info<see cref="SerializationInfo"/>.</param>
        /// <param name="context">The context<see cref="StreamingContext"/>.</param>
        public XMLFileLoadCreateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
