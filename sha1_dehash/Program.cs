using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sha1_dehash
{
    class Program
    {

        // calculate SHA1 hash
        // edit of code snippet from: http://securityblog.gr/1554/calculate-sha1-hash-from-string-in-c/
        public static String SHA1(String plaintext)
        {
            try
            {
                System.Security.Cryptography.SHA1 sha1 = System.Security.Cryptography.SHA1.Create();
                byte[] bytes = Encoding.ASCII.GetBytes(plaintext);
                byte[] hash = sha1.ComputeHash(bytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                    sb.Append(hash[i].ToString("x2"));

                return sb.ToString();
            }
            catch
            {
                Console.WriteLine("failed");
                return null;
            }
        }

        // constants
        public const string unsalted = "36D4F9BCACD78EEAF8358067713F5A3FDC62F44C";
        public const string salt = "SX";
        public const string salted = "SX36D4F9BCACD78EEAF8358067713F5A3FDC62F44C";

        // compare hash with given value
        public static String compareHashes(String s)
        {
            String value = s.ToLower();

            for(int i=0; i<10000; i++)
            {
                String pin = i.ToString("D4");

                //Console.WriteLine("current pin: " + pin);

                if ( SHA1(salt + pin) == value)
                    return salt + pin;
                //else if ( SHA1(pin + salt) == value)
                //    return pin + salt;
            }

            return null;
        }

        // main
        static void Main(string[] args)
        {
            String myPIN = compareHashes(unsalted);
            Console.WriteLine("given: " + salted);
            Console.WriteLine("generated: " + SHA1(myPIN));
            Console.WriteLine("my pin is: " + myPIN);

            while (true) ;
        }
    }

}
