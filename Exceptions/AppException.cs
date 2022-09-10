using BookShop.Areas.Api.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BookShop.Exceptions
{
    public class AppException : Exception
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public StatusCodeEnum ApiStatusCode { get; set; }
        public object AdditionalData { get; set; }

        public AppException(StatusCodeEnum statusCode, string message, HttpStatusCode httpStatusCode, Exception exception, object additionalData)
           : base(message, exception)
        {
            ApiStatusCode = statusCode;
            HttpStatusCode = httpStatusCode;
            AdditionalData = additionalData;
        }

        public AppException()
            : this(StatusCodeEnum.ServerError)
        {
        }

        public AppException(StatusCodeEnum statusCode)
            : this(statusCode, null)
        {
        }

        public AppException(string message)
            : this(StatusCodeEnum.ServerError, message)
        {
        }

        public AppException(StatusCodeEnum statusCode, string message)
            : this(statusCode, message, HttpStatusCode.InternalServerError)
        {
        }

        public AppException(string message, object additionalData)
            : this(StatusCodeEnum.ServerError, message, additionalData)
        {
        }

        public AppException(StatusCodeEnum statusCode, object additionalData)
            : this(statusCode, null, additionalData)
        {
        }

        public AppException(StatusCodeEnum statusCode, string message, object additionalData)
            : this(statusCode, message, HttpStatusCode.InternalServerError, additionalData)
        {
        }

        public AppException(StatusCodeEnum statusCode, string message, HttpStatusCode httpStatusCode)
            : this(statusCode, message, httpStatusCode, null)
        {
        }

        public AppException(StatusCodeEnum statusCode, string message, HttpStatusCode httpStatusCode, object additionalData)
            : this(statusCode, message, httpStatusCode, null, additionalData)
        {
        }

        public AppException(string message, Exception exception)
            : this(StatusCodeEnum.ServerError, message, exception)
        {
        }

        public AppException(string message, Exception exception, object additionalData)
            : this(StatusCodeEnum.ServerError, message, exception, additionalData)
        {
        }

        public AppException(StatusCodeEnum statusCode, string message, Exception exception)
            : this(statusCode, message, HttpStatusCode.InternalServerError, exception)
        {
        }

        public AppException(StatusCodeEnum statusCode, string message, Exception exception, object additionalData)
            : this(statusCode, message, HttpStatusCode.InternalServerError, exception, additionalData)
        {
        }

        public AppException(StatusCodeEnum statusCode, string message, HttpStatusCode httpStatusCode, Exception exception)
            : this(statusCode, message, httpStatusCode, exception, null)
        {
        }

    }
}
