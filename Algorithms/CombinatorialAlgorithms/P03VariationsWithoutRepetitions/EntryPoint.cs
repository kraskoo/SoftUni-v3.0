namespace P03VariationsWithoutRepetitions
{
    using System;

    public class EntryPoint
    {
        private static string[] items;
        private static string[] variations;
        private static bool[] used;
        private static int k;

        public static void Main()
        {
            items = Console.ReadLine().Split();
            k = int.Parse(Console.ReadLine());
            variations = new string[k];
            used = new bool[items.Length];
            Gen(0);
        }

        private static void Gen(int index)
        {
            if (index == k)
            {
                Console.WriteLine(string.Join(" ", variations));
            }
            else
            {
                for (int i = 0; i < items.Length; i++)
                {
                    if (!used[i])
                    {
                        used[i] = true;
                        variations[index] = items[i];
                        Gen(index + 1);
                        used[i] = false;
                    }
                }
            }
        }
    }
}