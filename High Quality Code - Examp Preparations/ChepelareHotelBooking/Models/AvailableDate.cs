namespace HotelBookingSystem.Models
{
    using System;
    using Interfaces;

    // Egyptian brackets FTW!
    public class AvailableDate : IAvailableDate
    {
        public AvailableDate(DateTime startDate, DateTime endDate)
        {
            this.ValidateDates(startDate, endDate);
            this.StartDate = startDate;
            this.EndDate = endDate;
        }

        public DateTime StartDate { get; }

        public DateTime EndDate { get; }

        private void ValidateDates(DateTime startDate, DateTime endDate)
        {
            if (endDate < startDate)
            {
                throw new ArgumentException("The date range is invalid.");
            }
        }
    }
}