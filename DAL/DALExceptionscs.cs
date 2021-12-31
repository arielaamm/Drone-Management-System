using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DALExceptionscs
{
    public class AlreadyExistException : Exception
    {
        public AlreadyExistException() : base() { }
        public AlreadyExistException(string message) : base(message) { }
        public AlreadyExistException(string message, Exception innerException)
            : base(message, innerException) { }
        public AlreadyExistException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
    public class DoesNotExistException : Exception
    {
        public DoesNotExistException() { }
        public DoesNotExistException(string message) : base(message) { }
        public DoesNotExistException(string message, Exception innerException) : base(message, innerException) { }
        protected DoesNotExistException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
