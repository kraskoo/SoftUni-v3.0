namespace Hierarchy.Core
{
    using System;
    using System.Collections.Generic;
    using System.Collections;
    using System.Linq;

    public class Hierarchy<T> : IHierarchy<T>
    {
        private readonly Node root;
        private readonly SortedDictionary<T, Node> children;

        private class Node
        {
            public Node(T value, Node parent = null)
            {
                this.Children = new HashSet<Node>();
                this.Value = value;
                this.Parent = parent;
            }

            public T Value { get; }

            public Node Parent { get; set; }

            public HashSet<Node> Children { get; }
        }

        public Hierarchy(T root)
        {
            this.root = new Node(root);
            this.children = new SortedDictionary<T, Node> { { root, this.root } };
        }

        public int Count => this.children.Count;

        public void Add(T element, T child)
        {
            if (!this.children.ContainsKey(element))
            {
                throw new ArgumentException();
            }

            if (this.children.ContainsKey(child))
            {
                throw new ArgumentException();
            }

            var parentNode = this.children[element];
            var childNode = new Node(child, parentNode);
            parentNode.Children.Add(childNode);
            this.children.Add(child, childNode);
        }

        public void Remove(T element)
        {
            if (!this.children.ContainsKey(element))
            {
                throw new ArgumentException();
            }

            var nodeToRemove = this.children[element];
            var parent = nodeToRemove.Parent;
            if (parent == null)
            {
                throw new InvalidOperationException();
            }

            var childChildren = nodeToRemove.Children;
            foreach (var child in childChildren)
            {
                child.Parent = parent;
                parent.Children.Add(child);
            }

            parent.Children.Remove(nodeToRemove);
            this.children.Remove(element);
        }

        public IEnumerable<T> GetChildren(T item)
        {
            if (!this.children.ContainsKey(item))
            {
                throw new ArgumentException();
            }

            var node = this.children[item];
            return node.Children.Select(child => child.Value);
        }

        public T GetParent(T item)
        {
            if (!this.Contains(item))
            {
                throw new ArgumentException();
            }

            var parent = this.children[item].Parent;
            if (parent == null)
            {
                return default(T);
            }

            return parent.Value;
        }

        public bool Contains(T value)
        {
            return this.children.ContainsKey(value);
        }

        public IEnumerable<T> GetCommonElements(IHierarchy<T> other)
        {
            return this.children.Values
                .Where(tc => other.Contains(tc.Value))
                .Select(tc => tc.Value);
        }

        public IEnumerator<T> GetEnumerator()
        {
            var queue = new Queue<Node>();
            queue.Enqueue(this.root);
            var current = queue.Dequeue();
            while (true)
            {
                foreach (var child in current.Children)
                {
                    queue.Enqueue(child);
                }

                yield return current.Value;
                if (queue.Count == 0)
                {
                    break;
                }

                current = queue.Dequeue();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}