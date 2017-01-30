using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modern_Password_Protection_Library
{
    internal class UserCredentials
    {
        #region Constructor
        internal UserCredentials(string login, byte[] hashedPassword, char[] individualPepperArray)
        {
            this.login = login;
            this.hashedPassword = hashedPassword;
            this.individualPepper = new String(individualPepperArray);
        }
        #endregion

        #region Fields
        internal string login;
        internal byte[] hashedPassword;
        internal string individualPepper; 
        #endregion
    }
}
