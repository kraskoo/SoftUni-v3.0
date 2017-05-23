namespace SortWords
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class EntryPoint
    {
        public static void Main()
        {
            List<string> strings = Console.ReadLine()
                .Trim()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(w => w.ToLower())
                .OrderBy(w => w)
                .ToList();
            Console.WriteLine(string.Join(" ", strings));
        }
    }
}