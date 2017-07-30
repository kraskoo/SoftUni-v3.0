namespace PH01PermutationWithoutRepetitions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class EntryPoint
    {
        public static void Main()
        {
            var input = Console.ReadLine().Split();
            int n = input.Length;
            int[] p = new int[input.Length];
            int i = 1;
            var result = new HashSet<string> { string.Join(" ", input) };
            while (i < n)
            {
                if (p[i] < i)
                {
                    int j = i % 2 == 0 ? 0 : p[i];
                    Swap(input, i, j);
                    result.Add(string.Join(" ", input));
                    p[i]++;
                    i = 1;
                }
                else
                {
                    p[i] = 0;
                    i++;
                }
            }

            Console.WriteLine(string.Join(Environment.NewLine, result.OrderBy(r => r)));
        }

        private static void Swap(string[] array, int firstIndex, int nextIndex)
        {
            var temp = array[firstIndex];
            array[firstIndex] = array[nextIndex];
            array[nextIndex] = temp;
        }
    }
}