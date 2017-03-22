namespace CarDealer.Models
{
    using System.Collections.Generic;

    public class Car : IModel
    {
        private ICollection<Part> parts;

        public Car()
        {
            this.parts = new HashSet<Part>();
        }

        public int Id { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public long TravelledDistance { get; set; }

        public virtual ICollection<Part> Parts
        {
            get { return this.parts; }
            set { this.parts = value; }
        }
    }
}