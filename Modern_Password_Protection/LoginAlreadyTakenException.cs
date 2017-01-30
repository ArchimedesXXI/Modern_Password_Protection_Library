using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modern_Password_Protection_Library
{
    public class LoginAlreadyTakenException : ArgumentException
    {
        public LoginAlreadyTakenException(string message, string paramName) : base(message, paramName)
        { }
    }
}
