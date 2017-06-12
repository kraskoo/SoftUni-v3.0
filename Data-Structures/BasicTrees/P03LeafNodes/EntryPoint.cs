namespace P03LeafNodes
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
            List<int> leafValue = new List<int>();
            GetChildren(Operations.GetRootNode(nodeByValue), leafValue);
            Console.WriteLine($"Leaf nodes: {string.Join(" ", leafValue.OrderBy(x => x))}");
        }

        private static void GetChildren(Tree<int> root, List<int> ints)
        {
            if (root.TreeChildren.Count == 0)
            {
                ints.Add(root.TreeValue);
                return;
            }

            foreach (var child in root.TreeChildren)
            {
                GetChildren(child, ints);
            }
        }
    }
}