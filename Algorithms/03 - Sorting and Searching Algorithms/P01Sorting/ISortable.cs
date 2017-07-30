namespace P01Sorting
{
    using System;

    public interface ISortable<in T> where T : IComparable<T>
    {
        /// <summary>
        /// Sort generic items of element
        /// </summary>
        /// <param name="array">elements</param>
        void Sort(params T[] array);

        /// <summary>
        /// return whether items is sorted or not.
        /// </summary>
        /// <param name="array">elements</param>
        /// <returns>return true if items is sorted or false if is not sorted.</returns>
        bool IsSorted(T[] array);
    }
}