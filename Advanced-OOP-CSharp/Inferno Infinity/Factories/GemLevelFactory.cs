namespace P10InfernoInfinity.Factories
{
    using System;
    using Interfaces;
    using Interfaces.Factories;
    using Models.Gems.GemLevels;

    public class GemLevelFactory : IGemLevelFactory
    {
        public IGemLevelable CreateGemLevel(string type)
        {
            switch (type)
            {
                case "Chipped":
                    return new ChippedGemLevel();
                case "Regular":
                    return new RegularGemLevel();
                case "Perfect":
                    return new PerfectGemLevel();
                case "Flawless":
                    return new FlawlessGemLevel();
                default:
                    throw new ArgumentException("Unknown type.");
            }
        }
    }
}