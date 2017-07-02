using System;

public class HeapExample
{
    static void Main()
    {
        // Start of heap sort demo

        var ints = new[] { 3, 11, 54, 1, -543, 23, 11113, 65 };
        Console.WriteLine("Before sort");
        Console.WriteLine(string.Join(", ", ints));
        Heap<int>.Sort(ints);
        Console.WriteLine("After sort");
        Console.WriteLine(string.Join(", ", ints));
        Console.WriteLine();

        // End of heap sort demo

        Console.WriteLine("Created an empty heap.");
        var heap = new BinaryHeap<int>();
        heap.Insert(5);
        heap.Insert(8);
        heap.Insert(1);
        heap.Insert(3);
        heap.Insert(12);
        heap.Insert(-4);

        Console.WriteLine("Heap elements (max to min):");
        while (heap.Count > 0)
        {
            var max = heap.Pull();
            Console.WriteLine(max);
        }
    }
}
