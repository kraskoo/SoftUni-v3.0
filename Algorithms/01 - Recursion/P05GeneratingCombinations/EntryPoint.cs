namespace P05GeneratingCombinations
{
    using System;
    using System.Linq;

    public class EntryPoint
    {
        public static void Main()
        {
            int[] numbers = { 1, 2, 3, 4 };
                    // Console.ReadLine()
                    // .Split()
                    // .Select(int.Parse)
                    // .ToArray();
            int k = 2; // int.Parse(Console.ReadLine());
            int[] vectors = new int[k];
            GenerateCombinations(numbers, vectors, 0, 0);
        }

        private static void GenerateCombinations(int[] numbers, int[] vectors, int index, int border)
        {
            if (index == vectors.Length)
            {
                Console.WriteLine(string.Join(" ", vectors));
            }
            else
            {
                for (int i = border; i < numbers.Length; i++)
                {
                    vectors[index] = numbers[i];
                    GenerateCombinations(numbers, vectors, index + 1, i + 1);
                }
            }
        }
    }
}