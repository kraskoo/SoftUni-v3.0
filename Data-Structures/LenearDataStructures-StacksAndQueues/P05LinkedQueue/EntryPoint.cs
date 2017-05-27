namespace P05LinkedQueue
{
    using System;

    public class EntryPoint
    {
        public static void Main()
        {
            LinkedQueue<int> queue = new LinkedQueue<int>();
            queue.Enqueue(2);
            queue.Enqueue(1590);
            queue.Enqueue(-34918);
            queue.Enqueue(-53);
            foreach (var node in queue)
            {
                Console.WriteLine(node);
            }

            Console.WriteLine();
            PrintArray(queue);
            Console.WriteLine();
            queue.Dequeue();
            PrintArray(queue);
            Console.WriteLine();
            queue.Dequeue();
            PrintArray(queue);
            Console.WriteLine();
            queue.Enqueue(11);
            PrintArray(queue);
            Console.WriteLine();
            queue.Dequeue();
            PrintArray(queue);
            Console.WriteLine();
            queue.Dequeue();
            PrintArray(queue);
            Console.WriteLine("----------");
            queue.Dequeue();
            PrintArray(queue);
        }

        private static void PrintArray(LinkedQueue<int> queue)
        {
            var toArray = queue.ToArray();
            foreach (int node in toArray)
            {
                Console.WriteLine(node);
            }
        }
    }
}