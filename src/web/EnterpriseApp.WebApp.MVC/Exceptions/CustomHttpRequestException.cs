using System;
using System.Net;

namespace EnterpriseApp.WebApp.MVC.Exceptions
{
    public class CustomHttpRequestException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }

        public CustomHttpRequestException(HttpStatusCode httpStatusCode)
        {
            StatusCode = httpStatusCode;
        }
    }
}
