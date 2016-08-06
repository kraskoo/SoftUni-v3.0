namespace P04ConvertFrom10ToBaseN
{
    using System;
    using System.Linq;
    using System.Numerics;

    public class ConvertFrom10ToBaseN
    {
        public static void Main()
        {
            BigInteger[] input =
                Console.ReadLine()?
                .Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                .Select(BigInteger.Parse)
                .ToArray();
            BigInteger nBase = input[0];
            BigInteger number = input[1];

            BigInteger convertedNumber = ConvertToN(number, nBase);
            Console.WriteLine(convertedNumber);
        }

        private static BigInteger ConvertToN(BigInteger number, BigInteger nBase)
        {
            int length = number.ToString().Length - 1;
            BigInteger collector = 0;
            for (int i = 0, j = length; i < length; i++, j--)
            {
                collector +=
                    BigInteger.Parse(number.ToString()[i].ToString()) * BigInteger.Pow(nBase, j);
            }

            collector += BigInteger.Parse(number.ToString()[length].ToString());
            return collector;
        }
    }
}