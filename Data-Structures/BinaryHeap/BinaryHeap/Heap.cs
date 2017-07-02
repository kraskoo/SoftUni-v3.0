using System;

public static class Heap<T> where T : IComparable<T>
{
    public static void Sort(T[] arr)
    {
        int halfLength = arr.Length / 2;
        for (int i = halfLength; i >= 0; i--)
        {
            HeapifyDown(arr, i, arr.Length);
        }

        for (int i = arr.Length - 1; i >= 0; i--)
        {
            Swap(arr, 0, i);
            HeapifyDown(arr, 0, i);
        }
    }

    private static void HeapifyDown(T[] arr, int index, int length)
    {
        int halfLength = length / 2;
        while (index < halfLength)
        {
            int child = 2 * index + 1;
            if (child + 1 < length && arr[child + 1].CompareTo(arr[child]) > 0)
            {
                child++;
            }

            if (arr[index].CompareTo(arr[child]) > 0)
            {
                break;
            }

            Swap(arr, index, child);
            index = child;
        }
    }

    private static void Swap(T[] arr, int index, int child)
    {
        T temp = arr[index];
        arr[index] = arr[child];
        arr[child] = temp;
    }
}
