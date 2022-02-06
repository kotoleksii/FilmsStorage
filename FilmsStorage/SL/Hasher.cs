using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Security.Cryptography;
using System.Text;

namespace FilmsStorage.SL
{
    public static class _SL 
    {
        public static class Hasher
        {
            public static byte[] createHash(string strToHash)
            {
                SHA512 hasher = SHA512.Create();

                //return hasher.ComputeHash(Encoding.UTF8.GetBytes(strToHash));
                byte[] hash = hasher.ComputeHash(Encoding.UTF8.GetBytes(strToHash));

                return hash;
            }
            //public checkPassword() 
        }
    }
    
}