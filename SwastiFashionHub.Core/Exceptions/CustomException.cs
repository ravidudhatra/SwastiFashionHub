using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SwastiFashionHub.Core.Exceptions
{
    public class CustomException : Exception
    {
        public List<string> ErrorMessages { get; } = new();
        public HttpStatusCode StatusCode { get; }
        public int CustomErrorCode { get; }

        public CustomException(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError, List<string> errors = default, int customErrorCode = 0) : base(message)
        {
            this.ErrorMessages = errors;
            this.StatusCode = statusCode;
            this.CustomErrorCode = customErrorCode;
        }

        public CustomException(Exception ex, string message, List<string> errors = default, HttpStatusCode statusCode = HttpStatusCode.InternalServerError) : base(message, ex)
        {
            this.ErrorMessages = errors;
            this.StatusCode = statusCode;
        }
    }
}
