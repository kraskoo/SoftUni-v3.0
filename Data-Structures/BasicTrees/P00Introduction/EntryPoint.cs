namespace P00Introduction
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BasicTree.Trees.Tree;

    public class EntryPoint
    {
        private static readonly Dictionary<int, Tree<int>> nodeByValue =
            new Dictionary<int, Tree<int>>();

        public static void Main()
        {
            ReadTreeValues(int.Parse(Console.ReadLine()));
            int searchedValue = int.Parse(Console.ReadLine());
            var root = GetRootNode();
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

        private static Tree<int> GetRootNode()
        {
            return nodeByValue?
                .Values?
                .Where(nv => nv.Parent == null)
                .FirstOrDefault();
        }

        private static void ReadTreeValues(int nodesCount)
        {
            for (int i = 0; i < nodesCount - 1; i++)
            {
                int[] couple = Console.ReadLine()
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();
                int parent = couple[0];
                int child = couple[1];
                Tree<int> parentNode = CreateNewNode(parent);
                Tree<int> childNode = CreateNewNode(child);
                parentNode.TreeChildren.Add(childNode);
                childNode.Parent = parentNode;
            }
        }

        private static Tree<int> CreateNewNode(int nodeValue)
        {
            if (!nodeByValue.ContainsKey(nodeValue))
            {
                nodeByValue.Add(nodeValue, new Tree<int>(nodeValue));
            }

            return nodeByValue[nodeValue];
        }
    }
}