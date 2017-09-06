namespace BoatRacingSimulator.Models.Boats
{
    using Engines;
    using Enumerations;
    using Utility;
    using Interfaces;

    public class Yacht : Boat
    {
        private int cargoWeight;

        public Yacht(
            string model,
            int weight,
            Engine engine,
            int cargoWeight) : base(
                model,
                weight,
                BoatType.Yacht)
        {
            this.Engine = engine;
            this.CargoWeight = cargoWeight;
        }

        public Engine Engine { get; set; }

        public int CargoWeight
        {
            get => this.cargoWeight;
            private set
            {
                Validator.ValidatePropertyValue(value, "Cargo Weight");
                this.cargoWeight = value;
            }
        }

        public override double CalculateRaceSpeed(IRace race)
        {
            return this.Engine.Output - this.Weight - this.CargoWeight + (race.OceanCurrentSpeed / 2d);
        }

        public override bool IsMotorBoat => true;
    }
}