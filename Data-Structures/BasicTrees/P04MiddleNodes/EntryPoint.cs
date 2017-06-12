namespace P04MiddleNodes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BasicTree.Trees.Tree;
    using CommonOperations;

    public class EntryPoint
    {
        private static readonly Dictionary<int, Tree<int>> nodeByValue =
            new Dictionary<int, Tree<int>>();
        private static readonly TreeOperations Operations = new TreeOperations();

        public static void Main()
        {
            Operations.ReadTreeValues(int.Parse(Console.ReadLine()), nodeByValue);
            Console.WriteLine($"Middle nodes: {string.Join(" ", GetMiddleNodes())}");
        }

        private static IEnumerable<int> GetMiddleNodes()
        {
            return nodeByValue
                .Values
                .Where(val => val.Parent != null && val.TreeChildren.Count > 0)
                .OrderBy(x => x.TreeValue)
                .Select(x => x.TreeValue);
        }
    }
}