namespace LongestSubsequence
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class EntryPoint
    {
        public static void Main()
        {
            List<int> ints = Console.ReadLine()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();
            int currentNumber = ints[0];
            int count = 1;
            List<int> currentLongestSequence = new List<int>(new[] { currentNumber });
            bool hasFoundNextNumber = false;
            int indexOfNextNumber = -1;
            for (int i = 1; i < ints.Count; i++)
            {
                if (ints[i] == currentNumber)
                {
                    currentLongestSequence.Add(ints[i]);
                    count++;
                }
                else if (!hasFoundNextNumber)
                {
                    hasFoundNextNumber = !hasFoundNextNumber;
                    indexOfNextNumber = i;
                }
            }

            while (hasFoundNextNumber)
            {
                hasFoundNextNumber = !hasFoundNextNumber;
                currentNumber = ints[indexOfNextNumber];
                List<int> nextSequences = new List<int>(new[] { currentNumber });
                for (int i = indexOfNextNumber + 1; i < ints.Count; i++)
                {
                    if (ints[i] == currentNumber)
                    {
                        nextSequences.Add(ints[i]);
                    }
                    else if (!hasFoundNextNumber)
                    {
                        hasFoundNextNumber = !hasFoundNextNumber;
                        indexOfNextNumber = i;
                    }
                }

                if (nextSequences.Count > count)
                {
                    count = nextSequences.Count;
                    currentLongestSequence = nextSequences;
                }
            }

            Console.WriteLine(string.Join(" ", currentLongestSequence));
        }
    }
}