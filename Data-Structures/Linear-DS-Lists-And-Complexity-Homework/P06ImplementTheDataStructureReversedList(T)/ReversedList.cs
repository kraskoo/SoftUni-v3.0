namespace P06ImplementTheDataStructureReversedList_T_
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class ReversedList<T> : IList<T>
    {
        private const int DefaultCapacity = 2;
        private T[] items;

        public ReversedList(int capacity = DefaultCapacity)
        {
            this.items = new T[capacity];
            this.Count = 0;
        }

        public int Count { get; private set; }

        public int Capacity => this.items.Length;

        public T this[int index]
        {
            get
            {
                this.ThrownIfIndexIsOutBoundaries(index);
                return this.items[this.Count - index - 1];
            }

            set
            {
                this.ThrownIfIndexIsOutBoundaries(index);
                this.items[this.Count - index - 1] = value;
            }
        }

        public void Add(T item)
        {
            if (this.Count == this.Capacity)
            {
                this.Resize();
            }

            this.items[this.Count++] = item;
        }

        public T RemoveAt(int index)
        {
            this.ThrownIfIndexIsOutBoundaries(index);
            int reversedIndex = this.Count - index - 1;
            var itemToRemove = this.items[reversedIndex];
            this.Shift(reversedIndex);
            this.Count--;
            return itemToRemove;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = this.Count - 1; i >= 0; i--)
            {
                yield return this.items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private void Shift(int index)
        {
            for (int i = index; i < this.Count - 1; i++)
            {
                this.items[i] = this.items[i + 1];
            }
        }

        private void Resize()
        {
            var newArray = new T[this.Capacity * 2];
            Array.Copy(this.items, newArray, this.Count);
            this.items = newArray;
        }

        private void ThrownIfIndexIsOutBoundaries(int index)
        {
            if (index < 0 || index >= this.Count)
            {
                throw new ArgumentOutOfRangeException(
                    "Index should be in list item's boundaries.");
            }
        }
    }
}