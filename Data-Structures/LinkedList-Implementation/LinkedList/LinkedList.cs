using System;
using System.Collections;
using System.Collections.Generic;

public class LinkedList<T> : IEnumerable<T>
{
    public LinkedList()
    {
        this.Count = 0;
    }

    public LinkedListNode Head { get; private set; }

    public LinkedListNode Tail { get; private set; }

    public int Count { get; private set; }

    public void AddFirst(T item)
    {
        var oldHead = this.Head;
        this.Head = new LinkedListNode(item);
        this.Head.Next = oldHead;
        if (this.IsEmpty())
        {
            this.Tail = this.Head;
        }

        this.Count++;
    }

    public void AddLast(T item)
    {
        var oldTail = this.Tail;
        this.Tail = new LinkedListNode(item);
        if (this.IsEmpty())
        {
            this.Head = this.Tail;
        }
        else
        {
            oldTail.Next = this.Tail;
        }

        this.Count++;
    }

    public T RemoveFirst()
    {
        if (this.IsEmpty())
        {
            throw new InvalidOperationException();
        }

        var nxtHead = this.Head.Value;
        this.Head = this.Head.Next;
        this.Count--;
        if (this.IsEmpty())
        {
            this.Tail = null;
        }

        return nxtHead;
    }

    public T RemoveLast()
    {
        if (this.IsEmpty())
        {
            throw new InvalidOperationException();
        }

        var lst = this.Tail.Value;
        if (this.Count == 1)
        {
            this.Head = this.Tail = null;
        }
        else
        {
            var preLast = this.GetPrelastElement();
            preLast.Next = null;
            this.Tail = preLast;
        }

        this.Count--;
        return lst;
    }

    public IEnumerator<T> GetEnumerator()
    {
        var current = this.Head;
        while (current != null)
        {
            yield return current.Value;
            current = current.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    public class LinkedListNode
    {
        public LinkedListNode(T value)
        {
            this.Value = value;
        }

        public T Value { get; set; }

        public LinkedListNode Next { get; set; }
    }

    private bool IsEmpty()
    {
        return this.Count == 0;
    }

    private LinkedListNode GetPrelastElement()
    {
        var current = this.Head;
        while (current.Next != this.Tail)
        {
            current = current.Next;
        }

        return current;
    }
}
