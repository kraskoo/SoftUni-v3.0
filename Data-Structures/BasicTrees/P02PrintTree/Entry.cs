namespace P02PrintTree
{
    using System;
    using System.Collections.Generic;
    using BasicTree.Trees.Tree;
    using CommonOperations;

    public class Entry
    {
        private static readonly Dictionary<int, Tree<int>> nodeByValue =
            new Dictionary<int, Tree<int>>();
        private static readonly TreeOperations Operations = new TreeOperations();

        public static void Main()
        {
            Operations.ReadTreeValues(int.Parse(Console.ReadLine()), nodeByValue);
            var root = Operations.GetRootNode(nodeByValue);
            root.Print();
        }
    }
}