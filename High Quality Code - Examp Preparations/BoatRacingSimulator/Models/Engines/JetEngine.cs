namespace BoatRacingSimulator.Models.Engines
{
    using Enumerations;

    public class JetEngine : Engine
    {
        private const int Multiplier = 5;
        private int cachedOutput;

        public JetEngine(
            string model,
            int horsepower,
            int displacement) : base(
                model,
                horsepower,
                displacement,
                EngineType.Jet)
        {
        }

        protected override int GenerateOutput()
        {
            return (this.HorsePower * Multiplier) + this.Displacement;
        }

        public override int Output
        {
            get
            {
                if (this.cachedOutput != 0)
                {
                    return this.cachedOutput;
                }

                this.cachedOutput = this.GenerateOutput();
                return this.cachedOutput;
            }
        }
    }
}