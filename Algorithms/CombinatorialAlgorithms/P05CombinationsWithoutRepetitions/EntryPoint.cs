namespace P05CombinationsWithoutRepetitions
{
    using System;

    public class EntryPoint
    {
        private static string[] sequence;
        private static string[] vector;

        public static void Main()
        {
            sequence = Console.ReadLine().Split();
            vector = new string[int.Parse(Console.ReadLine())];
            Gen(0, 0);
        }

        private static void Gen(int index, int start)
        {
            if (index == vector.Length)
            {
                Console.WriteLine(string.Join(" ", vector));
            }
            else
            {
                for (int i = start; i < sequence.Length; i++)
                {
                    vector[index] = sequence[i];
                    Gen(index + 1, i + 1);
                }
            }
        }
    }
}