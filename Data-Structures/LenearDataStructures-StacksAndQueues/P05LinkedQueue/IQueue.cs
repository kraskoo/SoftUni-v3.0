namespace P05LinkedQueue
{
    using System.Collections.Generic;

    public interface IQueue<T> : IEnumerable<T>
    {
        int Count { get; }

        void Enqueue(T element);

        T Dequeue();

        T[] ToArray();
    }
}