namespace SumAndAvarage
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class EntryPoint
    {
        public static void Main()
        {
            List<int> sequence = Console.ReadLine()
                .Trim()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();

            int sum = sequence.Sum();
            double avarage = sequence.Any() ? (double)sum / sequence.Count : 0;
            Console.WriteLine($"Sum={sum}; Average={avarage}");
        }
    }
}