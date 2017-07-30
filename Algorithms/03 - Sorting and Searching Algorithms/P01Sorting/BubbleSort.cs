namespace P01Sorting
{
    using System;

    public class BubbleSort<T> : Sorter<T> where T : IComparable<T>
    {
        public override void Sort(params T[] array)
        {
            this.Sort(array.Length, array);
        }

        private void Sort(int n, params T[] array)
        {
            var bound = n - 1;
            var newBound = 0;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < bound; j++)
                {
                    if (array[j].CompareTo(array[j + 1]) > 0)
                    {
                        var temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                        newBound = j;
                    }
                }

                bound = newBound;
            }
        }
    }
}