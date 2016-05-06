using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chieviewer
{
    public class ChieUtil
    {
        public static string DecryptNickName(string encryptedName)
        {
            const string normalCode = "abcdefghijklmnopqrstuvwxyz0123456789_";
            const string cryptCode  = "0123456789abcdefghijklmnopqrstuvwxyz_";

            string decodedString = "";

            foreach(var c in encryptedName)
            {
                int cryptPos = cryptCode.IndexOf(c);
                if (cryptPos < 0) return null;

                string normalChar = normalCode.Substring(cryptPos, 1);

                decodedString += normalChar;
            }

            return decodedString;
        }
    }
}
