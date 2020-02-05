﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SimpliPassApi.Exceptions
{
    public class SimpliPassException : Exception
    {
        public HttpStatusCode ErrorCode { get; }

        public SimpliPassException() : base()
        {
            ErrorCode = HttpStatusCode.BadRequest;
        }

        public SimpliPassException(string message) : base(message)
        {
            ErrorCode = HttpStatusCode.BadRequest;
        }

        public SimpliPassException(string message, Exception innerException) : base(message, innerException)
        {
            ErrorCode = HttpStatusCode.BadRequest;
        }
    }
}
