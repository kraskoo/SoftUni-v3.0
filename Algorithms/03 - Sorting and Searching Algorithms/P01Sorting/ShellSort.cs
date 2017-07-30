namespace P01Sorting
{
    using System;

    public class ShellSort<T> : Sorter<T>
        where T : IComparable<T>
    {
        public override void Sort(params T[] array)
        {
            int n = array.Length;

            for (int gap = n / 2; gap > 0; gap /= 2)
            {
                for (int i = gap; i < n; i++)
                {
                    var temp = array[i];
                    int j;
                    for (j = i; j >= gap && array[j - gap].CompareTo(temp) > 0; j -= gap)
                    {
                        array[j] = array[j - gap];
                    }

                    array[j] = temp;
                }
            }
        }
    }
}