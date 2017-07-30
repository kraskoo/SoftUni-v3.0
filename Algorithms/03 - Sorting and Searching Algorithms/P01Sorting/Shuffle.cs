namespace P01Sorting
{
    using System;

    public class Shuffler<T> : IShuffleable<T>
        where T : IComparable<T>
    {
        public void Shuffle(T[] array)
        {
            var rnd = new Random();
            for (int i = array.Length - 1; i >= 0; i--)
            {
                int nextIndex = rnd.Next(0, i);
                var temp = array[i];
                array[i] = array[nextIndex];
                array[nextIndex] = temp;
            }
        }
    }
}