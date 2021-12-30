﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BLExceptions
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
    public class NotTheRightForm : Exception
    {
        public NotTheRightForm() :base() { }

        public NotTheRightForm(string message) : base(message) { }

        public NotTheRightForm(string message, Exception innerException) : base(message, innerException){ }
        protected NotTheRightForm(SerializationInfo info, StreamingContext context) : base(info, context) { }

    }
}
