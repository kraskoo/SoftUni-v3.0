namespace P03ImplementAnArrayBasedStack
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class ArrayStack<T> : IStack<T>
    {
        private const int InitialCapacity = 16;
        private T[] typedArray;

        public ArrayStack(int capacity = InitialCapacity)
        {
            this.typedArray = new T[capacity];
            this.Count = 0;
        }

        public int Count { get; private set; }

        public void Push(T item)
        {
            if (this.Count == this.typedArray.Length)
            {
                this.Grow();
            }

            this.typedArray[this.Count++] = item;
        }

        public T Pop()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("Stack is empty");
            }

            var lastItem = this.typedArray[--this.Count];
            return lastItem;
        }

        public T[] ToArray()
        {
            var copiedArray = new T[this.Count];
            for (int i = 0; i < this.Count; i++)
            {
                copiedArray[i] = this.typedArray[this.Count - i - 1];
            }

            return copiedArray;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = this.Count - 1; i >= 0; i--)
            {
                yield return this.typedArray[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private void Grow()
        {
            var newArray = new T[this.typedArray.Length * 2];
            for (int i = 0; i < this.Count; i++)
            {
                newArray[i] = this.typedArray[i];
            }

            this.typedArray = newArray;
        }
    }
}