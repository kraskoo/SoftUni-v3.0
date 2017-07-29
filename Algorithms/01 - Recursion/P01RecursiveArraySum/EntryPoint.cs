namespace P01RecursiveArraySum
{
    using System;
    using System.Linq;

    public class EntryPoint
    {
        public static void Main()
        {
            var numbers = new[] { 1, 2, 3, 4 };
                    //Console.ReadLine()
                    //.Split()
                    //.Select(int.Parse)
                    //.ToArray();
            var sum = SumNumbers(numbers, numbers.Length - 1);
            Console.WriteLine(sum);
        }

        private static int SumNumbers(int[] numbers, int index)
        {
            if (index == 0)
            {
                return numbers[0];
            }

            return numbers[index] + SumNumbers(numbers, index - 1);
        }
    }
}