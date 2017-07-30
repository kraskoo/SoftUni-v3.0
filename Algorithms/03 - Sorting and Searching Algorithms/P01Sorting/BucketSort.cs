namespace P01Sorting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Own covariant.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BucketSort<T> : Sorter<T>
        where T : IComparable<T>
    {
        public override void Sort(params T[] array)
        {
            var flowLength = array.Length * 0.3;
            var bucketLength = (array.Length / (flowLength / 100)) % array.Length;
            var bucketCount = (int) (Math.Abs(bucketLength - flowLength) + 1);
            bucketCount =
                bucketCount > 10000 ?
                    bucketCount / 1000 :
                    bucketCount > 1000 ?
                        bucketCount / 100 :
                        bucketCount > 100 ?
                            bucketCount / 10 :
                            bucketCount;
            this.Sort(bucketCount, array);
        }

        private void Sort(int bucketCount, params T[] array)
        {
            var flowLength = (array.Length / bucketCount) + bucketCount;
            var buckets = new List<T>[bucketCount];
            for (int i = 0; i < bucketCount; i++)
            {
                buckets[i] = new List<T>(flowLength);
            }

            ISortable<T> sorter;
            if (array.Length / bucketCount < 100)
            {
                sorter = new InsertionSort<T>();
            }
            else
            {
                sorter = new QuickSort<T>();
            }

            for (int i = 0; i < array.Length; i++)
            {
                var intValue = int.Parse(
                    string.Join(
                        string.Empty,
                        array[i].ToString().ToCharArray().Select(c => Math.Abs(c - '0'))));
                var index = ((long) intValue + array.Length - 1) % array.Length;
                var bucketIndex = (int)(index / bucketCount) % bucketCount;
                buckets[bucketIndex].Add(array[i]);
            }

            foreach (List<T> bucket in buckets)
            {
                var bucketArray = bucket.ToArray();
                sorter.Sort(bucketArray);
                this.CopySortedElements(bucketArray, bucket);
            }

            int arrayIndex = -1;
            var bucketCounts = Enumerable.Repeat(0, bucketCount).ToArray();
            while (arrayIndex != array.Length - 1)
            {
                for (int i = 0; i < bucketCount; i++)
                {
                    var skip = bucketCounts[i] * bucketCount;
                    var elements = buckets[i].Skip(skip).Take(bucketCount);
                    bucketCounts[i]++;
                    foreach (var element in elements)
                    {
                        array[++arrayIndex] = element;
                    }
                }
            }
        }

        private void CopySortedElements(T[] bucketArray, List<T> list)
        {
            for (int i = 0; i < bucketArray.Length; i++)
            {
                list[i] = bucketArray[i];
            }
        }
    }
}