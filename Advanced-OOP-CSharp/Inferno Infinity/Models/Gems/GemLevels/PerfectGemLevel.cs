namespace P10InfernoInfinity.Models.Gems.GemLevels
{
    using System;
    using Enums;

    public class PerfectGemLevel : GemLevel
    {
        private const GemEnumerationLevel GemLevel = GemEnumerationLevel.Perfect;
        private const int DefaultLevelValues = 5;

        public PerfectGemLevel(string gemLevel) : base(
            DefaultLevelValues, DefaultLevelValues, DefaultLevelValues)
        {
            base.LevelOfGem = base.ParseGemLevel(this.ValidateGemLevel(gemLevel));
        }

        public PerfectGemLevel() : base(
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