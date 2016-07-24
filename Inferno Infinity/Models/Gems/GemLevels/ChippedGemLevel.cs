namespace P10InfernoInfinity.Models.Gems.GemLevels
{
    using System;
    using Enums;

    public class ChippedGemLevel : GemLevel
    {
        private const GemEnumerationLevel GemLevel = GemEnumerationLevel.Chipped;
        private const int DefaultLevelValues = 1;

        public ChippedGemLevel(string gemLevel) : base(
            DefaultLevelValues, DefaultLevelValues, DefaultLevelValues)
        {
            base.LevelOfGem = base.ParseGemLevel(this.ValidateGemLevel(gemLevel));
        }

        public ChippedGemLevel() : base(
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