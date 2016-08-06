namespace P10InfernoInfinity.Models.Gems.GemTypes
{
    using System;
    using Enums;

    public class RubyGemType : GemType
    {
        private const GemEnumerationType GemType = GemEnumerationType.Ruby;
        private const int DefaultStrength = 7;
        private const int DefaultAgility = 2;
        private const int DefaultVitality = 5;

        public RubyGemType(string gemType) : base(
            DefaultStrength, DefaultAgility, DefaultVitality)
        {
            base.TypeOfGem = base.ParseGemType(this.ValidateGemType(gemType));
        }


        public RubyGemType() : base(
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