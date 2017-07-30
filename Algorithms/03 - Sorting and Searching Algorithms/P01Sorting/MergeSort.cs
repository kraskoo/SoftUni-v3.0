namespace P01Sorting
{
    using System;

    public class MergeSort<T> : Sorter<T>
        where T : IComparable<T>
    {
        private T[] temproary;

        public override void Sort(params T[] array)
        {
            this.temproary = new T[array.Length];
            this.Sort(0, array.Length - 1, array);
        }

        private void Sort(int lo, int hi, params T[] array)
        {
            if (lo >= hi)
            {
                return;
            }

            int mid = (lo + hi) / 2;
            this.Sort(lo, mid, array);
            this.Sort(mid + 1, hi, array);
            this.Merge(lo, mid, hi, array);
        }

        private void Merge(int lo, int mid, int hi, T[] array)
        {
            if (array[mid].CompareTo(array[mid + 1]) < 0)
            {
                return;
            }

            Array.Copy(array, lo, temproary, lo, hi - lo + 1);
            int leftIndex = lo;
            int rightIndex = mid + 1;
            for (int index = lo; index <= hi; index++)
            {
                if (leftIndex > mid)
                {
                    array[index] = temproary[rightIndex++];
                }
                else if (rightIndex > hi)
                {
                    array[index] = temproary[leftIndex++];
                }
                else if (temproary[leftIndex].CompareTo(temproary[rightIndex]) < 0)
                {
                    array[index] = temproary[leftIndex++];
                }
                else
                {
                    array[index] = temproary[rightIndex++];
                }
            }
        }
    }
}