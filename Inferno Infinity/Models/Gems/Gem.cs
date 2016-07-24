namespace P10InfernoInfinity.Models.Gems
{
    using Enums;
    using Interfaces;

    public class Gem : IGem
    {
        private readonly IGemTypeable gemTypeable;
        private readonly IGemLevelable gemLevelable;

        public Gem(IGemTypeable gemTypeable, IGemLevelable gemLevelable)
        {
            this.gemTypeable = gemTypeable;
            this.gemLevelable = gemLevelable;
            this.Strength = this.gemTypeable.Strength + this.gemLevelable.Strength;
            this.Agility = this.gemTypeable.Agility + this.gemLevelable.Agility;
            this.Vitality = this.gemTypeable.Vitality + this.gemLevelable.Vitality;
        }

        public int Strength { get; }

        public int Agility { get; }

        public int Vitality { get; }
    }
}