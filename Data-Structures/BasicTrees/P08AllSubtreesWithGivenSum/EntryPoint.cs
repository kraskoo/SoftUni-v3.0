namespace P08AllSubtreesWithGivenSum
{
    using System;
    using System.Collections.Generic;
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
            int searchedValue = int.Parse(Console.ReadLine());
            var root = Operations.GetRootNode(nodeByValue);
            foreach (var tree in GetPathsWithSum(root, searchedValue))
            {
                PrintInPreOrder(tree);
                Console.WriteLine();
            }
        }

        private static void PrintInPreOrder(Tree<int> tree)
        {
            Console.Write($"{tree.TreeValue} ");
            foreach (var child in tree.TreeChildren)
            {
                PrintInPreOrder(child);
            }
        }

        private static List<Tree<int>> GetPathsWithSum(Tree<int> root, int searchedValue)
        {
            Console.WriteLine($"Subtrees of sum {searchedValue}: ");
            var roots = new List<Tree<int>>();
            GetSubtreePathsWithSum(root, searchedValue, 0, roots);
            return roots;
        }

        private static int GetSubtreePathsWithSum(
            Tree<int> node,
            int searchedValue,
            int current,
            List<Tree<int>> roots)
        {
            if (node == null)
            {
                return 0;
            }

            current = node.TreeValue;
            foreach (var treeChild in node.TreeChildren)
            {
                current += GetSubtreePathsWithSum(treeChild, searchedValue, current, roots);
            }

            if (current == searchedValue)
            {
                roots.Add(node);
            }

            return current;
        }
    }
}