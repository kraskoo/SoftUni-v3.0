namespace P02RecursiveFactorial
{
    using System;

    public class EntryPoint
    {
        public static void Main()
        {
            long num = 5;// long.Parse(Console.ReadLine());
            long factorial = Factorial(num);
            Console.WriteLine(factorial);
        }

        private static long Factorial(long num)
        {
            if (num <= 1)
            {
                return 1;
            }

            return num * Factorial(num - 1);
        }
    }
}