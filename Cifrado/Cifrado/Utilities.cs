using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
namespace Cifrado
{
    class Utilities
    {
        String chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public String ConvertToBase(BigInteger input, int radix)
        {
            if (input < 0) throw new ArgumentOutOfRangeException("input", input, "input cannot be negative");

            char[] clistarr = chars.ToCharArray();
            var result = new Stack<char>();
            while (input != 0)
            {
                result.Push(clistarr[(int)input % 36]);
                input /= 36;
            }
            return new string(result.ToArray());
        }

        public BigInteger ToDecimal(string Base36Value)
        {
       
            if (Base36Value == "")
            {
                return 0;
            }

            Base36Value = Base36Value.ToUpper();
            bool isNegative = false;

            if (Base36Value[0] == '-')
            {
                Base36Value = Base36Value.Substring(1);
                isNegative = true;
            }

            try
            {
                BigInteger returnValue = Base36DigitToNumber(Base36Value[Base36Value.Length - 1]);

                for (int i = 1; i < Base36Value.Length; i++)
                {
                    returnValue += ((long)Math.Pow(36, i) * Base36DigitToNumber(Base36Value[Base36Value.Length - (i + 1)]));
                }

                if (isNegative)
                {
                    return returnValue * -1;
                }
                else
                {
                    return returnValue;
                }
            }
            catch
            {
                return 0;
            }
        }

        private static byte Base36DigitToNumber(char Base36Digit)
        {
            if (!char.IsLetterOrDigit(Base36Digit))
            {
                return 0;
            }

            if (char.IsDigit(Base36Digit))
            {
                return byte.Parse(Base36Digit.ToString());
            }
            else
            {
                return (byte)((int)Base36Digit - 55);
            }
        }

        public string ConvertToBinary(BigInteger value)
        {
            string binary = "";
            if (value > 0)
            {
                while (value > 0)
                {
                    if (value % 2 == 0)
                    {
                        binary = "0" + binary;
                    }
                    else
                    {
                        binary = "1" + binary;
                    }
                    value = (int)value / 2;
                }
            }

            return binary;
        }

    }
}
