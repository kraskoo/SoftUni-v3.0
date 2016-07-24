namespace P10InfernoInfinity.Models.Gems.GemLevels
{
    using System;
    using Enums;

    public class FlawlessGemLevel : GemLevel
    {
        private const GemEnumerationLevel GemLevel = GemEnumerationLevel.Flawless;
        private const int DefaultLevelValues = 10;

        public FlawlessGemLevel(string gemLevel) : base(
            DefaultLevelValues, DefaultLevelValues, DefaultLevelValues)
        {
            base.LevelOfGem = base.ParseGemLevel(this.ValidateGemLevel(gemLevel));
        }

        public FlawlessGemLevel() : base(
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