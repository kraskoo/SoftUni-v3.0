namespace P03LongestSubsequence
{
    using System;
    using System.Linq;

    public class EntryPoint
    {
        public static void Main()
        {
            var ints = Console.ReadLine()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
            int maxNumber = 0;
            int maxCount = 0;
            for (int i = 0; i < ints.Length; i++)
            {
                int innerCount = 1;
                int currentNumber = ints[i];
                for (int j = i + 1; j < ints.Length; j++)
                {
                    if (ints[i] == ints[j])
                    {
                        innerCount++;
                    }
                    else
                    {
                        break;
                    }
                }

                if (innerCount > maxCount)
                {
                    maxNumber = currentNumber;
                    maxCount = innerCount;
                }
            }

            Console.WriteLine(string.Join(" ", Enumerable.Repeat(maxNumber, maxCount)));
        }
    }
}