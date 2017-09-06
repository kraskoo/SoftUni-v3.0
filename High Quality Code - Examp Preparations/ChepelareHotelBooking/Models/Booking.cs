namespace HotelBookingSystem.Models
{
    using System;
    using Interfaces;

    public class Booking : AvailableDate, IBooking
    {
        private decimal totalPrice;

        public Booking(
            IUser client,
            DateTime startBookDate,
            DateTime endBookDate,
            decimal totalPrice,
            string comments) : base(startBookDate, endBookDate)
        {
            this.Client = client;
            this.TotalPrice = totalPrice;
            this.Comments = comments;
        }

        public int Id { get; set; }

        public IUser Client { get; }

        public string Comments { get; }

        public decimal TotalPrice
        {
            get
            {
                return this.totalPrice;
            }

            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException("The total price must not be less than 0.");
                }

                this.totalPrice = value;
            }
        }
    }
}