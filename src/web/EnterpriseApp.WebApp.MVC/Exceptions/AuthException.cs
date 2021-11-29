using System;
using System.Collections.Generic;

namespace EnterpriseApp.WebApp.MVC.Exceptions
{
    public class AuthException : Exception
    {
        public AuthException() { }

        public AuthException(string message) : base(message) { }

        public AuthException(IEnumerable<string> messages)
            : this(string.Join("\n",messages))
        {
            
        }
    }
}
