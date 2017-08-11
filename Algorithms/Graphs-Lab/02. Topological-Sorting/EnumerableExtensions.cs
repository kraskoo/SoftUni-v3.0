namespace _02.Topological_Sorting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class EnumerableExtensions
    {
        public static void ForEachTerminal<T>(this IEnumerable<T> source, Action<T> action)
        {
            var array = source.ToArray();
            foreach (var element in array)
            {
                action(element);
            }
        }
    }
}