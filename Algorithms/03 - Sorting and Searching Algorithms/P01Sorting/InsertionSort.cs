namespace P01Sorting
{
    using System;

    public class InsertionSort<T> : Sorter<T>
        where T : IComparable<T>
    {
        public override void Sort(params T[] array)
        {
            for (int i = 1; i < array.Length; i++)
            {
                var x = array[i];
                var j = i - 1;
                while (j >= 0 && array[j].CompareTo(x) > 0)
                {
                    array[j + 1] = array[j];
                    j = j - 1;
                }

                array[j + 1] = x;
            }
        }
    }
}