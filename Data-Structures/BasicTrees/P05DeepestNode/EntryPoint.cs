namespace P05DeepestNode
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
            int maxDepth = 0;
            Tree<int> deepestNode = null;
            GetDeepestNode(Operations.GetRootNode(nodeByValue), 1, ref maxDepth, ref deepestNode);
            Console.WriteLine($"Deepest node: {deepestNode.TreeValue}");
        }

        private static void GetDeepestNode(Tree<int> root, int depth, ref int maxDepth, ref Tree<int> deepestNode)
        {
            if (root == null)
            {
                return;
            }

            if (depth > maxDepth)
            {
                deepestNode = root;
                maxDepth = depth;
            }

            foreach (var treeChild in root.TreeChildren)
            {
                GetDeepestNode(treeChild, depth + 1, ref maxDepth, ref deepestNode);
            }
        }
    }
}