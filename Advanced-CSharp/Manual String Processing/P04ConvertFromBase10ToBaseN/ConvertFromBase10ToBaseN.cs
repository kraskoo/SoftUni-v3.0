namespace P04ConvertFromBase10ToBaseN
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;
    using System.Text;

    public class ConvertFromBase10ToBaseN
    {
        public static void Main()
        {
            BigInteger[] data =
                Console.ReadLine()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(BigInteger.Parse)
                .ToArray();

            BigInteger nBase = data[0];
            BigInteger number = data[1];
            string convertedNumber = ConvertToNBase(number, nBase);
            Console.WriteLine(convertedNumber);
        }

        private static string ConvertToNBase(BigInteger number, BigInteger nBase)
        {
            if (number < nBase)
            {
                return number.ToString();
            }

            StringBuilder outputNumber = new StringBuilder();

            while (number > 0)
            {
                var rimender = number % nBase;
                outputNumber.Append(rimender);
                number /= nBase;
            }

            IEnumerable<char> reversed = outputNumber.ToString().Reverse();
            return string.Join("", reversed);
        }
    }
}