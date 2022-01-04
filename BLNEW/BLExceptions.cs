using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BLExceptions
{
    [Serializable]
    public class AlreadyExistException : Exception
    {
        public AlreadyExistException() : base() { }
        public AlreadyExistException(string message) : base(message) { }
        public AlreadyExistException(string message, Exception innerException)
            : base(message, innerException) { }
        public AlreadyExistException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }

    [Serializable]
    public class NotTheRightForm : Exception
    {
        public NotTheRightForm() :base() { }
        public NotTheRightForm(string message) : base(message) { }
        public NotTheRightForm(string message, Exception innerException) : base(message, innerException) { }
        protected NotTheRightForm(SerializationInfo info, StreamingContext context) : base(info, context) { }

    }

    [Serializable]
    public class DoesNotExistException : Exception
    {
        public DoesNotExistException() { }
        public DoesNotExistException(string message) : base(message) { }
        public DoesNotExistException(string message, Exception innerException) : base(message, innerException) { }
        protected DoesNotExistException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    [Serializable]
    public class PutTheRightNumber : Exception
    {
        public PutTheRightNumber() { }
        public PutTheRightNumber(string message) : base(message) { }
        public PutTheRightNumber(string message, Exception innerException) : base(message, innerException) { }
        protected PutTheRightNumber(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
    [Serializable]
    public class DroneAloreadyAttached : Exception
    {
        public DroneAloreadyAttached() { }
        public DroneAloreadyAttached(string message) : base(message) { }
        public DroneAloreadyAttached(string message, Exception innerException) : base(message, innerException) { }
        public DroneAloreadyAttached(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
