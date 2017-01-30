using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Modern_Password_Protection_Library
{
    public class CryptoTools
    {
        internal static readonly Random randomGenerator = new Random(); 


        internal static byte[] GenerateFullHashedPassword(ref char[] originalPassword, char[] Salt, char[] Pepper)
        {
            // salting and peppering the password
            char[] SaltedAndPepperedPassword = new char[originalPassword.Length + Salt.Length + Pepper.Length];
            originalPassword.CopyTo(SaltedAndPepperedPassword, 0);
            Salt.CopyTo(SaltedAndPepperedPassword, originalPassword.Length);
            Pepper.CopyTo(SaltedAndPepperedPassword, originalPassword.Length + Salt.Length);

            // hashing the full password (for multiple iterations)
            byte[] fullHashedPassword = CryptoTools.HashText(SaltedAndPepperedPassword);

            // deleting the content of the array of chars with the original passwords 
            CryptoTools.ClearOriginalArray(ref originalPassword);
            CryptoTools.ClearOriginalArray(ref SaltedAndPepperedPassword);

            return fullHashedPassword;
        }


        internal static byte[] HashText(char[] unhashedInput)
        {
            byte[] inputAsBytes = Encoding.ASCII.GetBytes(unhashedInput);
            // I am using the SHA256 hashing algorithm
            SHA256Managed hashingEngine = new SHA256Managed();
            // Here I perform only a couple iterations of password HASHING. 
            // In real life the recommended number of hashing iterations is 10,000 to 100,000.
            byte[] hashedBytes = inputAsBytes;
            for (int i=0; i<5; i++)
            {
                hashedBytes = hashingEngine.ComputeHash(hashedBytes);
            }

            return hashedBytes;
        }


        internal static char[] CreateSalt(int minLength, int maxLength)
        {
            int lenghtOfSalt = randomGenerator.Next(minLength, maxLength);
            char[] Salt = new char[lenghtOfSalt];

            for (int i = 0; i < lenghtOfSalt; i++)
            {
                // ASCII numbers corresponding to: letters, numbers and signs - are between 33 and 126
                int ASCIInumber = randomGenerator.Next(33, 126);
                Salt[i] = (char)ASCIInumber;
            }

            return Salt;
        }


        public static void ClearOriginalArray(ref char[] originalArray)
        {
            for (int i = 0; i < originalArray.Length; i++)
            {
                originalArray[i] = ' ';
            }
        }


    }
}
