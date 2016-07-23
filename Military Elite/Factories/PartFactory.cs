namespace P08MilitaryElite.Factories
{
    using Interfaces;
    using Interfaces.Factories;
    using Models;

    public class PartFactory : IPartFactory
    {
        public IPart CreatePart(string partName, int workedHours)
        {
            return new Part(partName, workedHours);
        }
    }
}