namespace P03CombinationsWithRepetition
{
    using System;
    using System.Linq;

    public class EntryPoint
    {
        public static void Main()
        {
            int n = 4;//int.Parse(Console.ReadLine());
            int k = 3;//int.Parse(Console.ReadLine());
            var sequence = Enumerable.Range(1, k).ToArray();
            CombinationWithRepetition(sequence, 0, 1, n);
        }

        private static void CombinationWithRepetition(
            int[] sequence, int index, int start, int endpoint)
        {
            if (index == sequence.Length)
            {
                Console.WriteLine(string.Join(" ", sequence));
                return;
            }

            for (int i = start; i <= endpoint; i++)
            {
                sequence[index] = i;
                CombinationWithRepetition(sequence, index + 1, start, endpoint);
                start++;
            }
        }
    }
}