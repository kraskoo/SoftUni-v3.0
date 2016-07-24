namespace P10InfernoInfinity.Models.Gems.GemLevels
{
    using System;
    using Enums;

    public class RegularGemLevel : GemLevel
    {
        private const GemEnumerationLevel GemLevel = GemEnumerationLevel.Regular;
        private const int DefaultLevelValues = 3;

        public RegularGemLevel(string gemLevel) : base(
            DefaultLevelValues, DefaultLevelValues, DefaultLevelValues)
        {
            base.LevelOfGem = base.ParseGemLevel(this.ValidateGemLevel(gemLevel));
        }

        public RegularGemLevel() : base(
            GemLevel, DefaultLevelValues, DefaultLevelValues, DefaultLevelValues)
        {
        }

        protected override sealed string ValidateGemLevel(string type)
        {
            if (type != GemLevel.ToString())
            {
                throw new ArgumentException(DefaultGemErrorLevelMessage);
            }

            return type;
        }
    }
}