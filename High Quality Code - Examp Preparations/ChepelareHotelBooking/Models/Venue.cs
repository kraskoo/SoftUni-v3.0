namespace HotelBookingSystem.Models
{
    using System;
    using System.Collections.Generic;
    using Interfaces;

    public class Venue : IVenue
    {
        private string name;
        private string address;

        public Venue(string name, string address, string description, IUser owner)
        {
            this.Name = name;
            this.Address = address;
            this.Description = description;
            this.Owner = owner;
            this.Rooms = new List<IRoom>();
        }

        public int Id { get; set; }

        public string Name
        {
            get
            {
                return this.name;
            }

            private set
            {
                if (string.IsNullOrEmpty(value) || value.Length < 3)
                {
                    throw new ArgumentException("The venue name must be at least 3 symbols long.");
                }

                this.name = value;
            }
        }

        public string Address
        {
            get
            {
                return this.address;
            }

            private set
            {
                if (string.IsNullOrEmpty(value) || value.Length < 3)
                {
                    throw new ArgumentException("The venue address must be at least 3 symbols long.");
                }

                this.address = value;
            }
        }

        public string Description { get; }

        public IUser Owner { get; }

        public ICollection<IRoom> Rooms { get; }
    }
}
