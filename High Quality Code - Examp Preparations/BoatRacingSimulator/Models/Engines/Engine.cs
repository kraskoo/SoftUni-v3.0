namespace BoatRacingSimulator.Models.Engines
{
    using Enumerations;
    using Interfaces;
    using Utility;

    public abstract class Engine : IEngine
    {
        private string model;
        private int horsePower;
        private int displacement;

        protected Engine(string model, int horsePower, int displacement, EngineType engineType)
        {
            this.Model = model;
            this.EngineType = engineType;
            this.HorsePower = horsePower;
            this.Displacement = displacement;
        }

        public int HorsePower
        {
            get => this.horsePower;
            private set
            {
                Validator.ValidatePropertyValue(value, "Horsepower");
                this.horsePower = value;
            }
        }

        public int Displacement
        {
            get => this.displacement;
            private set
            {
                Validator.ValidatePropertyValue(value, "Displacement");
                this.displacement = value;
            }
        }

        public string Model
        {
            get => this.model;
            private set
            {
                Validator.ValidateModelLength(value, Constants.MinBoatEngineModelLength);
                this.model = value;
            }
        }

        public EngineType EngineType { get; }

        protected abstract int GenerateOutput();

        public abstract int Output { get; }

        public override string ToString()
        {
            return $"{this.EngineType} - {this.Model}";
        }
    }
}