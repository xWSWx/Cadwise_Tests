using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmulatorATM
{
    public static class ATMutils
    {
        public static string GenerateRandomNumberString(int length)
        {
            Random random = new Random();
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                sb.Append(random.Next(0, 10)); // Generates a random number between 0 and 9
            }

            return sb.ToString();
        }
    }
}
