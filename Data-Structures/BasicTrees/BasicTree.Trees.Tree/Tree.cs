namespace BasicTree.Trees.Tree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Tree<T>
    {
        public Tree(T value, params Tree<T>[] children)
        {
            this.TreeValue = value;
            this.TreeChildren = new List<Tree<T>>();
            foreach (var treeChild in children)
            {
                this.TreeChildren.Add(treeChild);
                treeChild.Parent = this;
            }
        }

        public Tree<T> Parent { get; set; }

        public T TreeValue { get; set; }

        public ICollection<Tree<T>> TreeChildren { get; set; }

        public void Print(int indent = 0)
        {
            Console.WriteLine($"{string.Join(string.Empty, Enumerable.Repeat(' ', indent))}{this.TreeValue}");
            foreach (var node in this.TreeChildren)
            {
                node.Print(indent + 2);
            }
        }

        public void Each(Action<T> action)
        {
            action(this.TreeValue);
            foreach (var childTree in this.TreeChildren)
            {
                childTree.Each(action);
            }
        }

        public IEnumerable<T> OrderDFS()
        {
            ICollection<T> orderedList = new List<T>();
            this.Dfs(this, orderedList);
            return orderedList;
        }

        public IEnumerable<T> OrderBFS()
        {
            var list = new List<T>();
            var queue = new Queue<Tree<T>>();
            queue.Enqueue(this);
            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                list.Add(current.TreeValue);
                foreach (var childNode in current.TreeChildren)
                {
                    queue.Enqueue(childNode);
                }
            }

            return list;
        }

        private void Dfs(Tree<T> tree, ICollection<T> orderedList)
        {
            foreach (var node in tree.TreeChildren)
            {
                this.Dfs(node, orderedList);
            }

            orderedList.Add(tree.TreeValue);
        }
    }
}