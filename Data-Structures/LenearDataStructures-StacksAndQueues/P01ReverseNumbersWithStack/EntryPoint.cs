namespace P01ReverseNumbersWithStack
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class EntryPoint
    {
        public static void Main()
        {
            var input = Console.ReadLine();
            var items = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (items.Any(i => !char.IsDigit(i[i.Length - 1])))
            {
                Console.WriteLine(input);
                return;
            }

            Stack<int> ints = new Stack<int>(items.Select(int.Parse));
            do
            {
                Console.Write($"{ints.Pop()} ");
            } while (ints.Count != 0);

            Console.WriteLine();
        }
    }
}