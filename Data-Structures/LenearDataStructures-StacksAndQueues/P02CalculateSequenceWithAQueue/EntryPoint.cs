namespace P02CalculateSequenceWithAQueue
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class EntryPoint
    {
        public static void Main()
        {
            StringBuilder output = new StringBuilder();
            Queue<int> sequence = new Queue<int>();
            int firstMember = int.Parse(Console.ReadLine());
            int index = 1;
            sequence.Enqueue(firstMember);
            while (index != 51)
            {
                int preFirst = sequence.Dequeue();
                output.Append((index % 50) != 0 ? $"{preFirst}, " : $"{preFirst}");
                index++;
                if (index < 19)
                {
                    sequence.Enqueue(preFirst + 1);
                    sequence.Enqueue((2 * preFirst) + 1);
                    sequence.Enqueue(preFirst + 2);
                }

            }
            
            Console.WriteLine(output.ToString());
        }
    }
}