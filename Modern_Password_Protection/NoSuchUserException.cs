using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modern_Password_Protection_Library
{
    public class NoSuchUserException : ArgumentException
    {
        public NoSuchUserException(string message, string paramName) : base(message, paramName)
        { }

    }
}
