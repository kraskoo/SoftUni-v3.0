namespace P06ImplementTheDataStructureReversedList_T_
{
    using System.Collections.Generic;

    public interface IList<T> : IEnumerable<T>
    {
        int Count { get; }

        int Capacity { get; }

        void Add(T item);

        T RemoveAt(int index);
    }
}