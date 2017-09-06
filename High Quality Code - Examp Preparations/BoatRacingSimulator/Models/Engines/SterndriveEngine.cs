namespace BoatRacingSimulator.Models.Engines
{
    using Enumerations;

    public class SterndriveEngine : Engine
    {
        private const int Multiplier = 7;
        private int cachedOutput;

        public SterndriveEngine(
            string model,
            int horsepower,
            int displacement) : base(
                model,
                horsepower,
                displacement,
                EngineType.Sterndrive)
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