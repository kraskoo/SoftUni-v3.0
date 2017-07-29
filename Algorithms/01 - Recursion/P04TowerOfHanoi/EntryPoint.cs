namespace P04TowerOfHanoi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class EntryPoint
    {
        private static int move;
        private static Stack<int> source;
        private static readonly Stack<int> destination = new Stack<int>();
        private static readonly Stack<int> spare = new Stack<int>();

        public static void Main()
        {
            int n = 5;// int.Parse(Console.ReadLine());
            source = new Stack<int>(Enumerable.Range(1, n).Reverse());
            PrintPegs();
            MoveDisks(n, source, destination, spare);
        }

        private static void PrintPegs()
        {
            Console.WriteLine($"Source: {string.Join(", ", source.Reverse())}");
            Console.WriteLine($"Destination: {string.Join(", ", destination.Reverse())}");
            Console.WriteLine($"Spare: {string.Join(", ", spare.Reverse())}");
            Console.WriteLine();
        }

        private static void MoveDisks(
            int bottomDisk,
            Stack<int> sourcePeg,
            Stack<int> destinationPeg,
            Stack<int> sparePeg)
        {
            if (bottomDisk < 1)
            {
                return;
            }

            MoveDisks(bottomDisk - 1, sourcePeg, sparePeg, destinationPeg);
            move++;
            destinationPeg.Push(bottomDisk);
            sourcePeg.Pop();
            Console.WriteLine($"Step #{move}: Moved disk");
            PrintPegs();
            MoveDisks(bottomDisk - 1, sparePeg, destinationPeg, sourcePeg);
        }
    }
}