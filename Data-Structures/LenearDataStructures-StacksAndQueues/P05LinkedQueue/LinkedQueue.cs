namespace P05LinkedQueue
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class LinkedQueue<T> : IQueue<T>
    {
        private QueueNode headQueueNode;
        private QueueNode tailQueueNode;

        public LinkedQueue()
        {
            this.Count = 0;
        }

        public int Count { get; private set; }

        public void Enqueue(T element)
        {
            var oldTail = this.tailQueueNode;
            this.tailQueueNode = new QueueNode(element);

            if (this.Count == 0)
            {
                this.headQueueNode = this.tailQueueNode;
            }
            else
            {
                oldTail.NextNode = this.tailQueueNode;
            }

            this.Count++;
        }

        public T Dequeue()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("Queue is empty");
            }

            var nextHead = this.headQueueNode;
            this.headQueueNode = nextHead.NextNode;
            this.Count--;
            if (this.Count == 0)
            {
                this.headQueueNode = this.tailQueueNode = null;
            }

            return nextHead;
        }

        public T[] ToArray()
        {
            var array = new T[this.Count];
            var index = 0;
            var current = this.headQueueNode;
            while (current != null)
            {
                array[index] = current.Value;
                index++;
                current = current.NextNode;
            }

            return array;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var thisArray = this.ToArray();
            foreach (var node in thisArray)
            {
                yield return node;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private class QueueNode
        {
            public QueueNode(T value)
            {
                this.Value = value;
            }

            public T Value { get; }

            public QueueNode NextNode { get; set; }

            public QueueNode PreviousNode { get; set; }

            public static implicit operator T(QueueNode queueNode)
            {
                return queueNode.Value;
            }

            public static implicit operator QueueNode(LinkedListNode<T> node)
            {
                var newQueueNode = new QueueNode(node.Value);
                return newQueueNode;
            }
        }
    }
}