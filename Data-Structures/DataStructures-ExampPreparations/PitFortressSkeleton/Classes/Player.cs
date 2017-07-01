namespace Classes
{
    using System;
    using Interfaces;

    public class Player : IPlayer
    {
        private const int MinPlayerRadius = 0;
        private int radius;

        public Player(string name, int radius)
        {
            this.Name = name;
            this.Radius = radius;
            this.Score = 0;
        }

        public int CompareTo(Player other)
        {
            var cmp = this.Score.CompareTo(other.Score);
            if (cmp == 0)
            {
                cmp = string.Compare(this.Name, other.Name, StringComparison.CurrentCulture);
            }

            return cmp;
        }

        public string Name { get; private set; }

        public int Radius
        {
            get => this.radius;

            private set
            {
                if (value < MinPlayerRadius)
                {
                    throw new ArgumentException();
                }

                this.radius = value;
            }
        }

        public int Score { get; set; }
    }
}
