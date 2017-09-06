namespace BoatRacingSimulator.Models.Boats
{
    using Utility;
    using Enumerations;
    using Interfaces;

    public class RowBoat : Boat
    {
        private int oars;

        public RowBoat(string model, int weight, int oars) : base(model, weight, BoatType.RowBoat)
        {
            this.Oars = oars;
        }

        public int Oars
        {
            get => this.oars;
            private set
            {
                Validator.ValidatePropertyValue(value, "Oars");
                this.oars = value;
            }
        }

        public override double CalculateRaceSpeed(IRace race)
        {
            return (this.Oars * 100) - this.Weight + race.OceanCurrentSpeed;
        }

        public override bool IsMotorBoat => false;
    }
}