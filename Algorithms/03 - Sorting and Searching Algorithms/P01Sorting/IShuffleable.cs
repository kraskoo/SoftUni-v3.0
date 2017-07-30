namespace P01Sorting
{
    using System;

    public interface IShuffleable<in T> where T : IComparable<T>
    {
        void Shuffle(T[] array);
    }
}