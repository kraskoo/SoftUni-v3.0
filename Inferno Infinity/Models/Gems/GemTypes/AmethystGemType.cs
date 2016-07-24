namespace P10InfernoInfinity.Models.Gems.GemTypes
{
    using System;
    using Enums;

    public class AmethystGemType : GemType
    {
        private const GemEnumerationType GemType = GemEnumerationType.Amethyst;
        private const int DefaultStrength = 2;
        private const int DefaultAgility = 8;
        private const int DefaultVitality = 4;

        public AmethystGemType(string gemType) : base(
            DefaultStrength, DefaultAgility, DefaultVitality)
        {
            base.TypeOfGem = base.ParseGemType(this.ValidateGemType(gemType));
        }


        public AmethystGemType() : base(
                GemType, DefaultStrength, DefaultAgility, DefaultVitality)
        {
        }

        protected override sealed string ValidateGemType(string type)
        {
            if (type != GemType.ToString())
            {
                throw new ArgumentException(DefaultErrorGemTypeMessage);
            }

            return type;
        }
    }
}