using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SwastiFashionHub.Shared.Core.Exceptions
{
    public class AppException : ApplicationException
    {
        public List<string> Exceptions { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string Id { get; set; }
        public AppException(List<string> exceptions, HttpStatusCode statusCode = HttpStatusCode.BadRequest, string id = null)
        {
            this.Exceptions = exceptions;
            this.StatusCode = statusCode;
            Id = id;
        }
    }
}
