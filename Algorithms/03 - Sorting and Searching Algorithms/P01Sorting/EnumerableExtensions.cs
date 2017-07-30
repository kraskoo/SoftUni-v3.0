namespace P01Sorting
{
    using System;
    using System.Collections.Generic;

    public static class EnumerableExtensions
    {
        public static void Print<T>(this IEnumerable<T> enumerable)
        {
            Console.WriteLine(string.Join(" ", enumerable));
        }
    }
}