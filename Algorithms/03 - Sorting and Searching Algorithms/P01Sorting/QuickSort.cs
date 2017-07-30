namespace P01Sorting
{
    using System;

    public class QuickSort<T> : Sorter<T>
        where T : IComparable<T>
    {
        public override void Sort(params T[] array)
        {
            this.Sort(0, array.Length - 1, array);
        }

        private void Sort(int start, int end, params T[] array)
        {
            if (start < end)
            {
                var pi = Partition(start, end, array);
                Sort(start, pi - 1, array);
                Sort(pi + 1, end, array);
            }
        }

        private int Partition(int start, int end, params T[] array)
        {
            var pivot = array[end];
            int i = start - 1;
            for (int j = start; j < end; j++)
            {
                if (array[j].CompareTo(pivot) <= 0)
                {
                    i++;
                    this.Swap(array, i, j);
                }
            }

            this.Swap(array, i + 1, end);
            return i + 1;
        }

        private void Swap(T[] array, int firstIndex, int secondIndex)
        {
            var temp = array[firstIndex];
            array[firstIndex] = array[secondIndex];
            array[secondIndex] = temp;
        }
    }
}