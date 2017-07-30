namespace P01Sorting
{
    using System;

    public abstract class Sorter<T> : ISortable<T>
        where T : IComparable<T>
    {
        public abstract void Sort(params T[] array);

        public bool IsSorted(T[] array)
        {
            bool isSorted = true;
            for (int index = 0; index < array.Length - 1; index++)
            {
                if (array[index + 1].CompareTo(array[index]) < 0)
                {
                    isSorted = false;
                    break;
                }
            }

            return isSorted;
        }
    }
}