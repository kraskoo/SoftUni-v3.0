namespace HotelBookingSystem.Models
{
    using System;
    using System.Collections.Generic;
    using Interfaces;

    public class Room : IRoom
    {
        private int places;
        private decimal pricePerDay;

        public Room(int places, decimal pricePerDay)
        {
            this.Places = places;
            this.PricePerDay = pricePerDay;
            this.Bookings = new List<IBooking>();
            this.AvailableDates = new List<IAvailableDate>();
        }

        public int Id { get; set; }

        public int Places
        {
            get
            {
                return this.places;
            }

            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException("The places must not be less than 0.");
                }

                this.places = value;
            }
        }

        public decimal PricePerDay
        {
            get
            {
                return this.pricePerDay;
            }

            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException("The price per day must not be less than 0.");
                }

                this.pricePerDay = value;
            }
        }

        public ICollection<IAvailableDate> AvailableDates { get; }

        public ICollection<IBooking> Bookings { get; }
    }
}