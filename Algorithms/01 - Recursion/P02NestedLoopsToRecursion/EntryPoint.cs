namespace P02NestedLoopsToRecursion
{
    using System;

    public class EntryPoint
    {
        public static void Main()
        {
            int num = 3; // int.Parse(Console.ReadLine());
            var sequence = new int[num];
            PrintSequences(sequence, 0);
        }

        private static void PrintSequences(int[] sequence, int index)
        {
            if (index == sequence.Length)
            {
                Console.WriteLine(string.Join(" ", sequence));
                return;
            }

            for (int i = 1; i <= sequence.Length; i++)
            {
                sequence[index] = i;
                PrintSequences(sequence, index + 1);
            }
        }
    }
}