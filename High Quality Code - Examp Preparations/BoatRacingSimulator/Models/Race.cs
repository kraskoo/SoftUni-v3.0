namespace BoatRacingSimulator.Models
{
    using System.Collections.Generic;
    using Exceptions;
    using Interfaces;
    using Utility;

    public class Race : IRace
    {
        private int distance;

        public Race(int distance, int windSpeed, int oceanCurrentSpeed, bool allowsMotorboats)
        {
            this.Distance = distance;
            this.WindSpeed = windSpeed;
            this.OceanCurrentSpeed = oceanCurrentSpeed;
            this.AllowsMotorboats = allowsMotorboats;
            this.RegisteredBoats = new Dictionary<string, IBoat>();
        }

        public int Distance
        {
            get => this.distance;
            private set
            {
                Validator.ValidatePropertyValue(value, "Distance");
                this.distance = value;
            }
        }

        public int WindSpeed { get; }

        public int OceanCurrentSpeed { get; }

        public bool AllowsMotorboats { get; }

        protected Dictionary<string, IBoat> RegisteredBoats { get; set; }

        public void AddParticipant(IBoat boat)
        {
            if (this.RegisteredBoats.ContainsKey(boat.Model))
            {
                throw new DuplicateModelException(Constants.DuplicateModelMessage);
            }

            this.RegisteredBoats.Add(boat.Model, boat);
        }

        public IList<IBoat> GetParticipants()
        {
            return new List<IBoat>(this.RegisteredBoats.Values);
        }
    }
}