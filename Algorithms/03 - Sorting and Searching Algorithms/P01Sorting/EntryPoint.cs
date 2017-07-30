namespace P01Sorting
{
    using System;
    using System.Diagnostics;
    using System.Linq;

    public class EntryPoint
    {
        public static void Main()
        {
            int[] ranged = ShuffledElements(250000);
            var sorter = new BucketSort<int>();
            sorter.Sort(ranged);
            Console.WriteLine(string.Join(" ", ranged));
            Console.WriteLine(sorter.IsSorted(ranged));
            return;
            // Result with 50000 items:
            //      Bubble sort elapsed ticks: 36513430
            //      Insert sort elapsed ticks: 10283819
            //      Merge sort elapsed ticks: 42184
            //      Quick sort elapsed ticks: 37942
            //      Shell sort elapsed ticks: 61992
            //      Bucket sort elapsed ticks: 270754
            int[] rangedArray = ShuffledElements(50000);
            ISortable<int> bubbleSorter = new BubbleSort<int>();
            ISortable<int> insertSorter = new InsertionSort<int>();
            ISortable<int> mergeSorter = new MergeSort<int>();
            ISortable<int> quickSorter = new QuickSort<int>();
            ISortable<int> shellSorter = new ShellSort<int>();
            ISortable<int> bucketSorter = new BucketSort<int>();
            Stopwatch stopwatch = new Stopwatch();
            PrintComparation(
                rangedArray,
                bubbleSorter,
                insertSorter,
                mergeSorter,
                quickSorter,
                shellSorter,
                bucketSorter,
                stopwatch);
        }

        private static void PrintComparation(
            int[] rangedArray,
            ISortable<int> bubbleSorter,
            ISortable<int> insertSorter,
            ISortable<int> mergeSorter,
            ISortable<int> quickSorter,
            ISortable<int> shellSorter,
            ISortable<int> bucketSorter,
            Stopwatch stopwatch)
        {
            long bubbleTicks = GetElapsedTicks(rangedArray, bubbleSorter, stopwatch);
            Console.WriteLine("Bubble sort is done!");
            long insertTicks = GetElapsedTicks(rangedArray, insertSorter, stopwatch);
            Console.WriteLine("Insertion sort is done!");
            long mergeTicks = GetElapsedTicks(rangedArray, mergeSorter, stopwatch);
            Console.WriteLine("Merge sort is done!");
            long quickTicks = GetElapsedTicks(rangedArray, quickSorter, stopwatch);
            Console.WriteLine("Quick sort is done!");
            long shellTicks = GetElapsedTicks(rangedArray, shellSorter, stopwatch);
            Console.WriteLine("Shell sort is done!");
            long bucketTicks = GetElapsedTicks(rangedArray, bucketSorter, stopwatch);
            Console.WriteLine("Bucket sort is done!");
            Console.WriteLine($"Bubble sort elapsed ticks: {bubbleTicks}");
            Console.WriteLine($"Insert sort elapsed ticks: {insertTicks}");
            Console.WriteLine($"Merge sort elapsed ticks: {mergeTicks}");
            Console.WriteLine($"Quick sort elapsed ticks: {quickTicks}");
            Console.WriteLine($"Shell sort elapsed ticks: {shellTicks}");
            Console.WriteLine($"Bucket sort elapsed ticks: {bucketTicks}");
        }

        private static long GetElapsedTicks(int[] array, ISortable<int> sorter, Stopwatch watch)
        {
            var copiedArray = new int[array.Length];
            Array.Copy(array, copiedArray, array.Length);
            watch.Start();
            sorter.Sort(copiedArray);
            watch.Stop();
            var ticks = watch.ElapsedTicks;
            watch.Restart();
            return ticks;
        }

        private static int[] ShuffledElements(int count)
        {
            IShuffleable<int> shuffler = new Shuffler<int>();
            var rangedArray = Enumerable.Range(1, count).ToArray();
            shuffler.Shuffle(rangedArray);
            return rangedArray;
        }
    }
}