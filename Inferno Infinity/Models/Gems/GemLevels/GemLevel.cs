namespace P10InfernoInfinity.Models.Gems.GemLevels
{
    using System;
    using Enums;
    using Interfaces;

    public abstract class GemLevel : IGemLevelable
    {
        protected const string DefaultGemErrorLevelMessage = "Wrong gem level.";

        protected GemLevel(int strength, int agility, int vitality)
        {
            this.Strength = strength;
            this.Agility = agility;
            this.Vitality = vitality;
        }

        protected GemLevel(
            GemEnumerationLevel levelOfGem,
            int strength,
            int agility,
            int vitality) : this(strength, agility, vitality)
        {
            this.LevelOfGem = levelOfGem;
        }

        public GemEnumerationLevel LevelOfGem { get; protected set; }

        public int Strength { get; protected set; }

        public int Agility { get; protected set; }

        public int Vitality { get; protected set; }

        protected abstract string ValidateGemLevel(string type);

        protected GemEnumerationLevel ParseGemLevel(string gemLevel)
        {
            return
                (GemEnumerationLevel)Enum
                    .Parse(
                        typeof(GemEnumerationLevel),
                        gemLevel
                    );
        }
    }
}