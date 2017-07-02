using System;
using System.Collections.Generic;

public class BinaryHeap<T> where T : IComparable<T>
{
    private readonly List<T> heap;

    public BinaryHeap()
    {
        this.heap = new List<T>();
    }

    public int Count => this.heap.Count;

    public void Insert(T item)
    {
        this.heap.Add(item);
        this.HeapifyUp(this.heap.Count - 1);
    }

    public T Peek() => this.heap[0];

    public T Pull()
    {
        if (this.Count <= 0)
        {
            throw new InvalidOperationException();
        }

        T element = this.heap[0];
        this.Swap(0, this.heap.Count - 1);
        this.heap.RemoveAt(this.heap.Count - 1);
        this.HeapifyDown(0);
        return element;
    }

    private void HeapifyDown(int index)
    {
        while (index < this.Count / 2)
        {
            int child = this.Left(index);
            if (this.HasChild(child + 1) && this.IsLess(child, child + 1))
            {
                child++;
            }

            if (this.IsLess(child, index))
            {
                break;
            }

            this.Swap(index, child);
            index = child;
        }
    }

    private bool HasChild(int index)
    {
        return this.Count > this.Left(index);
    }

    private int Left(int index)
    {
        return (2 * index) + 1;
    }

    private void HeapifyUp(int index)
    {
        while (index > 0 && this.IsLess(this.Parent(index), index))
        {
            this.Swap(index, this.Parent(index));
            index = this.Parent(index);
        }
    }

    private void Swap(int index, int parent)
    {
        var temp = this.heap[parent];
        this.heap[parent] = this.heap[index];
        this.heap[index] = temp;
    }

    private bool IsLess(int parent, int index)
    {
        return this.heap[parent].CompareTo(this.heap[index]) < 0;
    }

    private int Parent(int index)
    {
        return (index - 1) / 2;
    }
}
