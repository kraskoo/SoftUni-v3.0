namespace BoatRacingSimulator.Models.Boats
{
    using System;
    using Enumerations;
    using Utility;
    using Interfaces;

    public class SailBoat : Boat
    {
        private int sailEfficiency;

        public SailBoat(string model, int weight, int sailEfficiency) : base(model, weight, BoatType.SailBoat)
        {
            this.SailEfficiency = sailEfficiency;
        }

        public int SailEfficiency
        {
            get => this.sailEfficiency;
            private set
            {
                if (value < 1 || value > 100)
                {
                    throw new ArgumentException(Constants.IncorrectSailEfficiencyMessage);
                }

                this.sailEfficiency = value;
            }
        }

        public override double CalculateRaceSpeed(IRace race)
        {
            return (race.WindSpeed * (this.SailEfficiency / 100d)) - this.Weight + (race.OceanCurrentSpeed / 2d);
        }

        public override bool IsMotorBoat => false;
    }
}