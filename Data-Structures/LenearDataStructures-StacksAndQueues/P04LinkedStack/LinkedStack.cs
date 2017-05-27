namespace P04LinkedStack
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class LinkedStack<T> : IStack<T>
    {
        private Node<T> firstNode;

        public LinkedStack()
        {
            this.Count = 0;
        }

        public int Count { get; private set; }

        public void Push(T item)
        {
            if (this.firstNode == null)
            {
                this.firstNode = new Node<T>(item);
            }
            else
            {
                var newNode = new Node<T>(item, firstNode);
                firstNode = newNode;
            }

            this.Count++;
        }

        public T Pop()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("Stack is empty");
            }

            var topNode = this.firstNode;
            this.firstNode = this.firstNode.NextNode;
            this.Count--;
            return topNode.Value;
        }

        public T[] ToArray()
        {
            var top = this.firstNode;
            var array = new T[this.Count];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = top.Value;
                top = top.NextNode;
            }

            return array;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var top = this.firstNode;
            while (top != null)
            {
                yield return top.Value;
                top = top.NextNode;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private class Node<T>
        {
            public Node(T value, Node<T> nextNode = null)
            {
                this.Value = value;
                this.NextNode = nextNode;

            }

            public T Value { get; }

            public Node<T> NextNode { get; set; }
        }
    }
}