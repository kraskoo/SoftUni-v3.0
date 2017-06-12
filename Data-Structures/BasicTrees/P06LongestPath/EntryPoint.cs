namespace P06LongestPath
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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
            var root = Operations.GetRootNode(nodeByValue);
            int currentMaxCount = 0;
            Stack<int> stack = new Stack<int>();
            Tree<int> child = null;
            GetLongestPath(root, ref currentMaxCount, ref child, ref stack);
            Console.WriteLine($"Longest path: {string.Join(" ", stack)}");
        }

        private static void GetLongestPath(
            Tree<int> root,
            ref int maxDepth,
            ref Tree<int> childNode,
            ref Stack<int> stack)
        {
            if (root == null)
            {
                return;
            }

            Tree<int> firstMax = null;
            if (root.TreeChildren.Count > 0)
            {
                int? maxChildrenCount = root.TreeChildren?
                    .Max(tc => tc.TreeChildren.Count);
                firstMax = root.TreeChildren
                    .Where(tc => tc.TreeChildren.Count == maxChildrenCount)?
                    .FirstOrDefault();
                if (maxChildrenCount > maxDepth)
                {
                    childNode = firstMax;
                    maxDepth = maxChildrenCount.Value;
                }
            }

            foreach (var treeChild in root.TreeChildren)
            {
                if (treeChild.TreeValue.Equals(firstMax?.TreeValue))
                {
                    GetLongestPath(
                        treeChild,
                        ref maxDepth,
                        ref childNode,
                        ref stack);
                }
            }

            if (root.TreeValue.Equals(childNode?.TreeValue))
            {
                stack.Push(childNode.TreeValue);
                childNode = childNode.Parent;
            }
            else
            {
                stack.Push(root.TreeValue);
            }
        }
    }
}