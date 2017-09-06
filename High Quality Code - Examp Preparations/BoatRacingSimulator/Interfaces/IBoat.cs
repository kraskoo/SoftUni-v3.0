namespace BoatRacingSimulator.Interfaces
{
    using Enumerations;

    public interface IBoat : IModelable
    {
        int Weight { get; }

        bool IsMotorBoat { get; }

        BoatType BoatType { get; }

        double CalculateRaceSpeed(IRace race);
    }
}