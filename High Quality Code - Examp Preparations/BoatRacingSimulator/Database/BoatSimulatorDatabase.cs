namespace BoatRacingSimulator.Database
{
    using Interfaces;

    public class BoatSimulatorDatabase : IBoatSimulatorDatabase
    {
        public BoatSimulatorDatabase()
        {
            this.Boats = new Repository<IBoat>();
            this.Engines = new Repository<IEngine>();
        }

        public IRepository<IBoat> Boats { get; }

        public IRepository<IEngine> Engines { get; }
    }
}