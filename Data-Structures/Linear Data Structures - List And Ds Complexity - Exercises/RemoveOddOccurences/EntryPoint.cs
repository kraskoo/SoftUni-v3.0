namespace RemoveOddOccurences
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class EntryPoint
    {
        public static void Main()
        {
            Dictionary<int, int> numbersCount = new Dictionary<int, int>();
            var ints = Console.ReadLine()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
            for (int i = 0; i < ints.Length; i++)
            {
                if (!numbersCount.ContainsKey(ints[i]))
                {
                    numbersCount.Add(ints[i], 1);
                }
                else
                {
                    numbersCount[ints[i]]++;
                }
            }

            ints = numbersCount
                .Where(kvp => kvp.Value % 2 == 0)
                .SelectMany(kvp => Enumerable.Repeat(kvp.Key, kvp.Value))
                .ToArray();
            Console.WriteLine(string.Join(" ", ints));
        }
    }
}