namespace CommonOperations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BasicTree.Trees.Tree;

    public class TreeOperations
    {
        public Tree<int> GetRootNode(Dictionary<int, Tree<int>> nodeByValue)
        {
            return nodeByValue?
                .Values?
                .Where(nv => nv.Parent == null)
                .FirstOrDefault();
        }

        public void ReadTreeValues(int nodesCount, Dictionary<int, Tree<int>> nodeByValue)
        {
            for (int i = 0; i < nodesCount - 1; i++)
            {
                int[] couple = Console.ReadLine()
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();
                int parent = couple[0];
                int child = couple[1];
                Tree<int> parentNode = CreateNewNode(parent, nodeByValue);
                Tree<int> childNode = CreateNewNode(child, nodeByValue);
                parentNode.TreeChildren.Add(childNode);
                childNode.Parent = parentNode;
            }
        }

        private Tree<int> CreateNewNode(int nodeValue, Dictionary<int, Tree<int>> nodeByValue)
        {
            if (!nodeByValue.ContainsKey(nodeValue))
            {
                nodeByValue.Add(nodeValue, new Tree<int>(nodeValue));
            }

            return nodeByValue[nodeValue];
        }
    }
}