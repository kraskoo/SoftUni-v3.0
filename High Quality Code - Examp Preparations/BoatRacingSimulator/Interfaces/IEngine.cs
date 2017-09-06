namespace BoatRacingSimulator.Interfaces
{
    using Enumerations;

    public interface IEngine : IModelable
    {
        int HorsePower { get; }

        int Displacement { get; }

        EngineType EngineType { get; }

        int Output { get; }
    }
}