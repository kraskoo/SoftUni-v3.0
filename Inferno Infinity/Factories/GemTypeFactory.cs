namespace P10InfernoInfinity.Factories
{
    using System;
    using Interfaces;
    using Interfaces.Factories;
    using Models.Gems.GemTypes;

    public class GemTypeFactory : IGemTypeFactory
    {
        public IGemTypeable CreateGemType(string type)
        {
            switch (type)
            {
                case "Ruby":
                    return new RubyGemType();
                case "Emerald":
                    return new EmeraldGemType();
                case "Amethyst":
                    return new AmethystGemType();
                default:
                    throw new ArgumentException("Unknown type.");
            }
        }
    }
}