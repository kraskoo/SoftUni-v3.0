using System.Collections.Generic;

namespace P02PermutationsWithRepetitions
{
    using System;

    public class EntryPoint
    {
        private static string[] sequence;

        public static void Main()
        {
            sequence = Console.ReadLine().Split();
            Gen(0);
        }

        private static void Gen(int index)
        {
            if (index == sequence.Length)
            {
                Console.WriteLine(string.Join(" ", sequence));
            }
            else
            {
                Gen(index + 1);
                var used = new HashSet<string> { sequence[index] };
                for (int i = index + 1; i < sequence.Length; i++)
                {
                    if (!used.Contains(sequence[i]))
                    {
                        used.Add(sequence[i]);
                        Swap(index, i);
                        Gen(index + 1);
                        Swap(index, i);
                    }
                }
            }
        }

        private static void Swap(int i, int j)
        {
            var temp = sequence[i];
            sequence[i] = sequence[j];
            sequence[j] = temp;
        }
    }
}