namespace _02.Topological_Sorting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class EnumerableExtensions
    {
        public static IEnumerable<T> Apply<T>(this IEnumerable<T> source, Func<T, bool> predicate = null)
        {
            var array = predicate != null ?
                source.Where(predicate).ToArray() :
                source.ToArray();
            foreach (var element in array)
            {
                yield return element;
            }
        }

        public static void ForEachTerminal<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var element in source)
            {
                action(element);
            }
        }

        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var element in source)
            {
                action(element);
                yield return element;
            }
        }
    }
}