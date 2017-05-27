namespace P03ImplementAnArrayBasedStack
{
    using System;
    using System.Collections.Generic;

    public class EntryPoint
    {
        public static void Main()
        {
            Stack<int> stack = new Stack<int>();
            stack.Push(3);
            stack.Push(-3453);
            stack.Push(1123);
            stack.Push(0);
            stack.Push(902);
            PrintEnumerable(stack);
            Console.WriteLine($"{stack.Pop()}, Count = {stack.Count}");
            Console.WriteLine($"{stack.Pop()}, Count = {stack.Count}");
            Console.WriteLine($"{stack.Pop()}, Count = {stack.Count}");
            Console.WriteLine($"{stack.Pop()}, Count = {stack.Count}");
            Console.WriteLine($"{stack.Pop()}, Count = {stack.Count}");

            Console.WriteLine();
            var arrayFromStack = stack.ToArray();
            PrintArray(arrayFromStack);

            ArrayStack<int> ints = new ArrayStack<int>();
            ints.Push(3);
            ints.Push(-3453);
            ints.Push(1123);
            ints.Push(0);
            ints.Push(902);
            Console.WriteLine();
            PrintEnumerable(ints);
            Console.WriteLine();
            var array = ints.ToArray();
            PrintArray(array);
            Console.WriteLine($"{ints.Pop()}, Count = {ints.Count}");
            Console.WriteLine($"{ints.Pop()}, Count = {ints.Count}");
            Console.WriteLine($"{ints.Pop()}, Count = {ints.Count}");
            Console.WriteLine($"{ints.Pop()}, Count = {ints.Count}");
            Console.WriteLine($"{ints.Pop()}, Count = {ints.Count}");
        }

        private static void PrintEnumerable(IEnumerable<int> stack)
        {
            foreach (var item in stack)
            {
                Console.WriteLine(item);
            }
        }

        private static void PrintArray(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                Console.WriteLine(array[i]);
            }
        }
    }
}