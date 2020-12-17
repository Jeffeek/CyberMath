using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberMath.Primitives.Int32.DivideExtension
{
    public static class Int32Extension
    {
        public static bool IsOdd(this int number) => number % 2 == 0;
        public static bool IsEven(this int number) => number % 2 != 0;

        public static bool IsPrime(this int number)
        {
            for (var i = 2; i <= Math.Sqrt(number); i++)
                if (number % i == 0)
                    return false;
            return true;
        }

        public static int GCD(this int a, int b)
        {
            if (a < b)
                Swap(ref a, ref b);
            while (b > 0)
            {
                a %= b;
                Swap(ref a, ref b);
            }

            return a;
        }

        public static int LCM(this int a, int b) => a / GCD(a, b) * b;

        public static void Swap(ref int a, ref int b)
        {
            int temp = a;
            a = b;
            b = temp;
        }

        public static int PercentOf(this int a, int b) => a / b * 100;

    }
}
