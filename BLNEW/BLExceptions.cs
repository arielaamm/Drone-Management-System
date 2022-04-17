using System;
using System.Runtime.Serialization;

namespace BLExceptions
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
        public AlreadyExistException(string message, Exception innerException)
            : base(message, innerException)
        {
            throw new AlreadyExistException(message);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AlreadyExistException"/> class.
        /// </summary>
        /// <param name="info">The info<see cref="SerializationInfo"/>.</param>
        /// <param name="context">The context<see cref="StreamingContext"/>.</param>
        public AlreadyExistException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }

    /// <summary>
    /// Defines the <see cref="NotTheRightFormException" />.
    /// </summary>
    [Serializable]
    public class NotTheRightFormException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotTheRightFormException"/> class.
        /// </summary>
        public NotTheRightFormException() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotTheRightFormException"/> class.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        public NotTheRightFormException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotTheRightFormException"/> class.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        /// <param name="innerException">The innerException<see cref="Exception"/>.</param>
        public NotTheRightFormException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotTheRightFormException"/> class.
        /// </summary>
        /// <param name="info">The info<see cref="SerializationInfo"/>.</param>
        /// <param name="context">The context<see cref="StreamingContext"/>.</param>
        public NotTheRightFormException(SerializationInfo info, StreamingContext context) : base(info, context)
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
        protected DoesNotExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    /// <summary>
    /// Defines the <see cref="PutTheRightNumberException" />.
    /// </summary>
    [Serializable]
    public class PutTheRightNumberException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PutTheRightNumberException"/> class.
        /// </summary>
        public PutTheRightNumberException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PutTheRightNumberException"/> class.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        public PutTheRightNumberException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PutTheRightNumberException"/> class.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        /// <param name="innerException">The innerException<see cref="Exception"/>.</param>
        public PutTheRightNumberException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PutTheRightNumberException"/> class.
        /// </summary>
        /// <param name="info">The info<see cref="SerializationInfo"/>.</param>
        /// <param name="context">The context<see cref="StreamingContext"/>.</param>
        protected PutTheRightNumberException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    /// <summary>
    /// Defines the <see cref="DroneAloreadyAttachedException" />.
    /// </summary>
    [Serializable]
    public class DroneAloreadyAttachedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DroneAloreadyAttachedException"/> class.
        /// </summary>
        public DroneAloreadyAttachedException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DroneAloreadyAttachedException"/> class.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        public DroneAloreadyAttachedException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DroneAloreadyAttachedException"/> class.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        /// <param name="innerException">The innerException<see cref="Exception"/>.</param>
        public DroneAloreadyAttachedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DroneAloreadyAttachedException"/> class.
        /// </summary>
        /// <param name="info">The info<see cref="SerializationInfo"/>.</param>
        /// <param name="context">The context<see cref="StreamingContext"/>.</param>
        public DroneAloreadyAttachedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    /// <summary>
    /// Defines the <see cref="DontHaveEnoughPowerException" />.
    /// </summary>
    [Serializable]
    public class DontHaveEnoughPowerException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DontHaveEnoughPowerException"/> class.
        /// </summary>
        public DontHaveEnoughPowerException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DontHaveEnoughPowerException"/> class.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        public DontHaveEnoughPowerException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DontHaveEnoughPowerException"/> class.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        /// <param name="innerException">The innerException<see cref="Exception"/>.</param>
        public DontHaveEnoughPowerException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DontHaveEnoughPowerException"/> class.
        /// </summary>
        /// <param name="info">The info<see cref="SerializationInfo"/>.</param>
        /// <param name="context">The context<see cref="StreamingContext"/>.</param>
        public DontHaveEnoughPowerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    /// <summary>
    /// Defines the <see cref="DroneDontInChargingException" />.
    /// </summary>
    [Serializable]
    public class DroneDontInChargingException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DroneDontInChargingException"/> class.
        /// </summary>
        public DroneDontInChargingException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DroneDontInChargingException"/> class.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        public DroneDontInChargingException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DroneDontInChargingException"/> class.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        /// <param name="innerException">The innerException<see cref="Exception"/>.</param>
        public DroneDontInChargingException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DroneDontInChargingException"/> class.
        /// </summary>
        /// <param name="info">The info<see cref="SerializationInfo"/>.</param>
        /// <param name="context">The context<see cref="StreamingContext"/>.</param>
        public DroneDontInChargingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    /// <summary>
    /// Defines the <see cref="ThereIsNoParcelToAttachException" />.
    /// </summary>
    [Serializable]
    public class ThereIsNoParcelToAttachException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ThereIsNoParcelToAttachException"/> class.
        /// </summary>
        public ThereIsNoParcelToAttachException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ThereIsNoParcelToAttachException"/> class.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        public ThereIsNoParcelToAttachException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ThereIsNoParcelToAttachException"/> class.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        /// <param name="innerException">The innerException<see cref="Exception"/>.</param>
        public ThereIsNoParcelToAttachException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ThereIsNoParcelToAttachException"/> class.
        /// </summary>
        /// <param name="info">The info<see cref="SerializationInfo"/>.</param>
        /// <param name="context">The context<see cref="StreamingContext"/>.</param>
        public ThereIsNoParcelToAttachException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    /// <summary>
    /// Defines the <see cref="AlreadyPickedUpException" />.
    /// </summary>
    [Serializable]
    public class AlreadyPickedUpException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AlreadyPickedUpException"/> class.
        /// </summary>
        public AlreadyPickedUpException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AlreadyPickedUpException"/> class.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        public AlreadyPickedUpException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AlreadyPickedUpException"/> class.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        /// <param name="innerException">The innerException<see cref="Exception"/>.</param>
        public AlreadyPickedUpException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AlreadyPickedUpException"/> class.
        /// </summary>
        /// <param name="info">The info<see cref="SerializationInfo"/>.</param>
        /// <param name="context">The context<see cref="StreamingContext"/>.</param>
        public AlreadyPickedUpException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    /// <summary>
    /// Defines the <see cref="ParcelPastErroeException" />.
    /// </summary>
    [Serializable]
    public class ParcelPastErroeException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParcelPastErroeException"/> class.
        /// </summary>
        public ParcelPastErroeException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParcelPastErroeException"/> class.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        public ParcelPastErroeException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParcelPastErroeException"/> class.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        /// <param name="innerException">The innerException<see cref="Exception"/>.</param>
        public ParcelPastErroeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParcelPastErroeException"/> class.
        /// </summary>
        /// <param name="info">The info<see cref="SerializationInfo"/>.</param>
        /// <param name="context">The context<see cref="StreamingContext"/>.</param>
        public ParcelPastErroeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    /// <summary>
    /// Defines the <see cref="DroneIsBusyException" />.
    /// </summary>
    [Serializable]
    public class DroneIsBusyException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DroneIsBusyException"/> class.
        /// </summary>
        public DroneIsBusyException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DroneIsBusyException"/> class.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        public DroneIsBusyException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DroneIsBusyException"/> class.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        /// <param name="innerException">The innerException<see cref="Exception"/>.</param>
        public DroneIsBusyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DroneIsBusyException"/> class.
        /// </summary>
        /// <param name="info">The info<see cref="SerializationInfo"/>.</param>
        /// <param name="context">The context<see cref="StreamingContext"/>.</param>
        public DroneIsBusyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    /// <summary>
    /// Defines the <see cref="ThereIsNoParcelToAttachdException" />.
    /// </summary>
    [Serializable]
    public class ThereIsNoParcelToAttachdException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ThereIsNoParcelToAttachdException"/> class.
        /// </summary>
        public ThereIsNoParcelToAttachdException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ThereIsNoParcelToAttachdException"/> class.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        public ThereIsNoParcelToAttachdException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ThereIsNoParcelToAttachdException"/> class.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        /// <param name="innerException">The innerException<see cref="Exception"/>.</param>
        public ThereIsNoParcelToAttachdException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ThereIsNoParcelToAttachdException"/> class.
        /// </summary>
        /// <param name="info">The info<see cref="SerializationInfo"/>.</param>
        /// <param name="context">The context<see cref="StreamingContext"/>.</param>
        public ThereIsNoParcelToAttachdException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    /// <summary>
    /// Defines the <see cref="CantDeleteException" />.
    /// </summary>
    [Serializable]
    public class CantDeleteException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CantDeleteException"/> class.
        /// </summary>
        public CantDeleteException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CantDeleteException"/> class.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        public CantDeleteException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CantDeleteException"/> class.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        /// <param name="innerException">The innerException<see cref="Exception"/>.</param>
        public CantDeleteException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CantDeleteException"/> class.
        /// </summary>
        /// <param name="info">The info<see cref="SerializationInfo"/>.</param>
        /// <param name="context">The context<see cref="StreamingContext"/>.</param>
        public CantDeleteException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    /// <summary>
    /// Defines the <see cref="DroneInActionException" />.
    /// </summary>
    [Serializable]
    public class DroneInActionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DroneInActionException"/> class.
        /// </summary>
        public DroneInActionException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DroneInActionException"/> class.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        public DroneInActionException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DroneInActionException"/> class.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        /// <param name="innerException">The innerException<see cref="Exception"/>.</param>
        public DroneInActionException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DroneInActionException"/> class.
        /// </summary>
        /// <param name="info">The info<see cref="SerializationInfo"/>.</param>
        /// <param name="context">The context<see cref="StreamingContext"/>.</param>
        public DroneInActionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    /// <summary>
    /// Defines the <see cref="DroneIsAlreadyChargeException" />.
    /// </summary>
    [Serializable]
    public class DroneIsAlreadyChargeException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DroneIsAlreadyChargeException"/> class.
        /// </summary>
        public DroneIsAlreadyChargeException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DroneIsAlreadyChargeException"/> class.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        public DroneIsAlreadyChargeException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DroneIsAlreadyChargeException"/> class.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        /// <param name="innerException">The innerException<see cref="Exception"/>.</param>
        public DroneIsAlreadyChargeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DroneIsAlreadyChargeException"/> class.
        /// </summary>
        /// <param name="info">The info<see cref="SerializationInfo"/>.</param>
        /// <param name="context">The context<see cref="StreamingContext"/>.</param>
        public DroneIsAlreadyChargeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
