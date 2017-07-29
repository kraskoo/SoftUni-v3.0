namespace P01ReverseArray
{
    using System;
    using System.Linq;

    public class EntryPoint
    {
        public static void Main()
        {
            var numbers = new[] { 1, 2, 3, 4 };
                    // Console.ReadLine()
                    // .Split()
                    // .Select(int.Parse)
                    // .ToArray();

            PrintArray(numbers, 0);
            Console.WriteLine();
        }

        private static void PrintArray(int[] numbers, int index)
        {
            if (index == numbers.Length)
            {
                return;
            }

            PrintArray(numbers, index + 1);
            Console.Write($"{numbers[index]} ");
        }
    }
}