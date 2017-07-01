namespace Classes
{
    using System;
    using Interfaces;

    public class Mine : IMine
    {
        private const int MaxDelay = 10_000;
        private const int MaxX = 1_000_000;
        private const int MaxDamage = 100;
        private int delay;
        private int xCoordinates;
        private int damage;

        public Mine(Player player, int id, int x, int delay, int damage)
        {
            this.Player = player;
            this.Id = id;
            this.XCoordinate = x;
            this.Delay = delay;
            this.Damage = damage;
        }

        public int CompareTo(Mine other)
        {
            var cmp = this.Delay.CompareTo(other.Delay);
            if (cmp == 0)
            {
                cmp = this.Id.CompareTo(other.Id);
            }

            return cmp;
        }

        public int Id { get; private set; }

        public int Delay
        {
            get => this.delay;

            set
            {
                if (value < 0 || value > MaxDelay)
                {
                    throw new ArgumentException();
                }

                this.delay = value;
            }
        }

        public int Damage
        {
            get => this.damage;

            private set
            {
                if (value < 0 || value > MaxDamage)
                {
                    throw new ArgumentException();
                }

                this.damage = value;
            }
        }

        public int XCoordinate
        {
            get => this.xCoordinates;

            private set
            {
                if (value < 0 || value > MaxX)
                {
                    throw new ArgumentException();
                }

                this.xCoordinates = value;
            }
        }

        public Player Player { get; private set; }
    }
}