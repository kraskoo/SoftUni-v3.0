namespace P04Generating0_1Vectors
{
    using System;

    public class EntryPoint
    {
        public static void Main()
        {
            int num = 3;// int.Parse(Console.ReadLine());
            int[] vectors = new int[num];
            Gen(vectors, 0);
        }

        private static void Gen(int[] vectors, int index)
        {
            if (index == vectors.Length)
            {
                Print(vectors);
                return;
            }

            for (int i = 0; i <= 1; i++)
            {
                vectors[index] = i; 
                Gen(vectors, index + 1);
            }
        }

        private static void Print(int[] vectors)
        {
            Console.WriteLine(string.Join(string.Empty, vectors));
        }
    }
}