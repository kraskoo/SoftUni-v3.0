namespace BoatRacingSimulator.Models.Boats
{
    using Enumerations;
    using Interfaces;

    public class PowerBoat : Boat
    {
        public PowerBoat(string model, int weight, IEngine engineA, IEngine engineB) : base(model, weight, BoatType.PowerBoat)
        {
            this.EngineA = engineA;
            this.EngineB = engineB;
        }

        public IEngine EngineA { get; }

        public IEngine EngineB { get; }

        public override double CalculateRaceSpeed(IRace race)
        {
            return this.EngineA.Output + this.EngineB.Output - this.Weight + (race.OceanCurrentSpeed / 5d);
        }

        public override bool IsMotorBoat => true;
    }
}