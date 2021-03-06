﻿namespace CarDealer.Models
{
    public class Sale : IModel
    {
        public int Id { get; set; }

        public virtual Car Car { get; set; }

        public virtual Customer Customer { get; set; }

        public double Discount { get; set; }
    }
}