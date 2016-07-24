namespace P10InfernoInfinity.Models.Gems.GemTypes
{
    using System;
    using Enums;
    using Interfaces;

    public abstract class GemType : IGemTypeable
    {
        protected const string DefaultErrorGemTypeMessage = "Wrong gem type.";

        protected GemType(int strength, int agility, int vitality)
        {
            this.Strength = strength;
            this.Agility = agility;
            this.Vitality = vitality;
        }

        protected GemType(
            GemEnumerationType typeOfGem,
            int strength,
            int agility,
            int vitality) : this(strength, agility, vitality)
        {
            this.TypeOfGem = typeOfGem;
        }

        public GemEnumerationType TypeOfGem { get; protected set; }

        public int Strength { get; protected set; }

        public int Agility { get; protected set; }

        public int Vitality { get; protected set; }

        protected abstract string ValidateGemType(string type);

        protected GemEnumerationType ParseGemType(string typeOfGem)
        {
            return
                (GemEnumerationType)Enum
                    .Parse(
                        typeof(GemEnumerationType),
                        typeOfGem
                    );
        }
    }
}