namespace P04LinkedStack
{
    using System.Collections.Generic;

    public interface IStack<T> : IEnumerable<T>
    {
        int Count { get; }

        void Push(T item);

        T Pop();

        T[] ToArray();
    }
}