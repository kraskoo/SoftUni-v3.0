namespace P07NChooseKCount
{
    using System;
    using System.Collections.Generic;

    public class EntryPoint
    {
        private static readonly Dictionary<string, decimal> FirstCoefficient = new Dictionary<string, decimal>();
        private static readonly Dictionary<string, decimal> SecondCoefficient = new Dictionary<string, decimal>();

        public static void Main()
        {
            int n = int.Parse(Console.ReadLine());
            int k = int.Parse(Console.ReadLine());
            Console.WriteLine(Binom(n, k));
        }

        private static decimal Binom(int n, int k)
        {
            if (n < k)
            {
                return 0;
            }

            if (k == 0 || k == n)
            {
                return 1;
            }

            if (!FirstCoefficient.ContainsKey($"{n - 1} {k - 1}"))
            {
                FirstCoefficient.Add($"{n - 1} {k - 1}", Binom(n - 1, k - 1));
            }

            if (!SecondCoefficient.ContainsKey($"{n - 1} {k}"))
            {
                SecondCoefficient.Add($"{n - 1} {k}", Binom(n - 1, k));
            }

            return FirstCoefficient[$"{n - 1} {k - 1}"] + SecondCoefficient[$"{n - 1} {k}"];
        }
    }
}