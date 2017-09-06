namespace BoatRacingSimulator.Models.Boats
{
    using Enumerations;
    using Interfaces;
    using Utility;

    public abstract class Boat : IBoat
    {
        private string model;
        private int weight;

        protected Boat(string model, int weight, BoatType boatType)
        {
            this.Model = model;
            this.Weight = weight;
            this.BoatType = boatType;
        }

        public BoatType BoatType { get; }

        public string Model
        {

            get => this.model;
            private set
            {
                Validator.ValidateModelLength(value, Constants.MinBoatModelLength);
                this.model = value;
            }
        }

        public int Weight
        {
            get => this.weight;
            private set
            {
                Validator.ValidatePropertyValue(value, "Weight");
                this.weight = value;
            }
        }

        public abstract double CalculateRaceSpeed(IRace race);

        public abstract bool IsMotorBoat { get; }
    }
}