using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Modern_Password_Protection_Library
{
    public class Password_Protection_Engine
    {
        #region Constructors
        public Password_Protection_Engine(DataBaseOptions dataBaseOption, string URLtoDataBaseFile)
        {
            if (dataBaseOption == DataBaseOptions.createNewDataBase)
            {
                this.dataBaseUsed = new DataBase(URLtoDataBaseFile);
            }
            else
            {
                //bool success = this.VerifyConnectionToDataBase(URLtoDataBaseFile);
                //if (success)
                //    this.dataBaseUsed = this.LoadDataBase(URLtoDataBaseFile);
                //else
                //    throw new ArgumentException("The Data Base under the given URL could not be reached!", nameof(URLtoDataBaseFile));
            }
        }
        #endregion

        #region enum with DataBase options
        public enum DataBaseOptions
        {
            createNewDataBase,
            accessExistingDataBase
        }
        #endregion

        #region Fields and Properties
        private DataBase dataBaseUsed; 
        #endregion


        #region Public Methods
        public void SaveNewPassword(string login, ref char[] originalPassword)
        {
            // salt for the password
            char[] Salt = dataBaseUsed.Salt;
            
            // pepper for the password
            char[] Pepper = CryptoTools.CreateSalt(5, 10);

            // We are calling a function, that does for us: 
            //  - salting and peppering the password,             
            //  - hashing the full password (multiple iterations), 
            //  - deleting the content of the array of chars with the original passwords. 
            byte[] fullHashedPassword = CryptoTools.GenerateFullHashedPassword(ref originalPassword, Salt, Pepper); 

            //store credentials in a data base
            UserCredentials newUser = new UserCredentials(login, fullHashedPassword, Pepper);
            dataBaseUsed.AddUser(newUser);
        }

        public bool VerifyPassword(string login, ref char[] inputtedPassword)
        {
            UserCredentials user = this.dataBaseUsed.FetchUser(login);
            if (user == null)
            {
                CryptoTools.ClearOriginalArray(ref inputtedPassword);
                throw new NoSuchUserException("The user with given login does not exist in the data base!", nameof(login));
            }

            // At this point we should have a user with the correct login. Let's ensure that by using Assertions: 
            Debug.Assert(login == user.login);

            //Fetching the Salt and the Pepper
            char[] Salt = this.dataBaseUsed.Salt;
            char[] Pepper = user.individualPepper.ToCharArray();

            // Generating full (salted, peppered) hashed password from original user password 
            byte[] inputtedHashedPassword = CryptoTools.GenerateFullHashedPassword(ref inputtedPassword,Salt, Pepper);

            // Verifying password: 
            if (Enumerable.SequenceEqual(user.hashedPassword, inputtedHashedPassword))
                return true;
            return false;
        }

        public void ChangePassword(string login, char[] oldPassword, char[] newPassword)
        {
            bool isPasswordCorrect = this.VerifyPassword(login, ref oldPassword);

            if (isPasswordCorrect)
                // TODO
                throw new NotImplementedException();
            else
                // TODO
                throw new NotImplementedException();
        }
        #endregion

        
    }
}
