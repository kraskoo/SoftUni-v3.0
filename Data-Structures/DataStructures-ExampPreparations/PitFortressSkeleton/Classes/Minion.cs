using System;

namespace Classes
{
    using Interfaces;

    public class Minion : IMinion
    {
        private const int MaxX = 1_000_000;
        private int xCoordinate;

        public Minion(int id, int x)
        {
            this.Id = id;
            this.XCoordinate = x;
            this.Health = 100;
        }

        public int CompareTo(Minion other)
        {
            var cmp = this.XCoordinate.CompareTo(other.XCoordinate);
            if (cmp == 0)
            {
                cmp = this.Id.CompareTo(other.Id);
            }

            return cmp;
        }

        public int Id { get; private set; }

        public int XCoordinate
        {
            get => this.xCoordinate;

            private set
            {
                if (value < 0 || value > MaxX)
                {
                    throw new ArgumentException();
                }

                this.xCoordinate = value;
            }
        }

        public int Health { get; set; }
    }
}