namespace P07AllPathsWithGivenSum
{
    using System;
    using System.Collections.Generic;
    using CommonOperations;
    using BasicTree.Trees.Tree;

    public class EntryPoint
    {
        private static readonly Dictionary<int, Tree<int>> nodeByValue =
            new Dictionary<int, Tree<int>>();
        private static readonly TreeOperations Operations = new TreeOperations();

        public static void Main()
        {
            Operations.ReadTreeValues(int.Parse(Console.ReadLine()), nodeByValue);
            int searchedValue = int.Parse(Console.ReadLine());
            var root = Operations.GetRootNode(nodeByValue);
            foreach (var leaf in GetPathsWithSum(root, searchedValue))
            {
                Print(leaf);
            }
        }

        private static void Print(Tree<int> leaf)
        {
            var stack = new Stack<int>();
            var current = leaf;
            while (current != null)
            {
                stack.Push(current.TreeValue);
                current = current.Parent;
            }

            Console.WriteLine(string.Join(" ", stack));
        }

        private static List<Tree<int>> GetPathsWithSum(Tree<int> root, int searchedValue)
        {
            Console.WriteLine($"Paths of sum {searchedValue}:");
            var leafs = new List<Tree<int>>();
            GetPathsWithSum(root, searchedValue, 0, leafs);
            return leafs;
        }

        private static void GetPathsWithSum(
            Tree<int> node,
            int searchedValue,
            int current,
            List<Tree<int>> leafs)
        {
            if (node == null)
            {
                return;
            }

            current += node.TreeValue;
            if (current == searchedValue && node.TreeChildren.Count == 0)
            {
                leafs.Add(node);
            }

            foreach (var treeChild in node.TreeChildren)
            {
                GetPathsWithSum(treeChild, searchedValue, current, leafs);
            }
        }
    }
}