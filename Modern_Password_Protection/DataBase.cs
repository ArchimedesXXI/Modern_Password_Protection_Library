using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Modern_Password_Protection_Library
{
    internal class DataBase
    {
        internal DataBase(string URLtoDataBaseFile)
        {
            this.URLtoDataBaseFile = URLtoDataBaseFile;
            // In real life SALTS should be long - about 32 random bytes long.
            // I am using short SALTS (5 to 10 characters long) because the code is written for learning purposes.
            this.Salt = CryptoTools.CreateSalt(5, 10);
            this.AllUserCredentials = new List<UserCredentials>();
        }

        internal string URLtoDataBaseFile;
        internal char[] Salt;
        internal List<UserCredentials> AllUserCredentials { get; private set; }


        internal void AddUser(UserCredentials newUser)
        {
            if (this.FetchUser(newUser.login) == null)
            {
                this.AllUserCredentials.Add(newUser);
            }
            else
            {
                throw new LoginAlreadyTakenException(
                    "The user with given login already exists. A different login needs to be chosen.", nameof(newUser));
            }
        }

        internal void RemoveUser()
        {
            // TODO
        }

        internal UserCredentials FetchUser(string login)
        {
            var QueryResult = 
                (from users 
                in this.AllUserCredentials
                where (users.login == login)
                select users);

            // At this point the query should be either empty or have only 1 user in it
            Debug.Assert(  (!QueryResult.Any())   ||   (QueryResult.Count() == 1)  );

            if (!QueryResult.Any())
                return null;
            else
                return QueryResult.ElementAt(0);
        } 

    }
}
