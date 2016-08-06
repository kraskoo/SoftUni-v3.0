namespace P10InfernoInfinity.Models.Gems.GemTypes
{
    using System;
    using Enums;

    public class EmeraldGemType : GemType
    {
        private const GemEnumerationType GemType = GemEnumerationType.Emerald;
        private const int DefaultStrength = 1;
        private const int DefaultAgility = 4;
        private const int DefaultVitality = 9;

        public EmeraldGemType(string gemType) : base(
            DefaultStrength, DefaultAgility, DefaultVitality)
        {
            base.TypeOfGem = base.ParseGemType(this.ValidateGemType(gemType));
        }


        public EmeraldGemType() : base(
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