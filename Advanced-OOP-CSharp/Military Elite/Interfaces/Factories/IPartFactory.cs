namespace P08MilitaryElite.Interfaces.Factories
{
    public interface IPartFactory
    {
        IPart CreatePart(string partName, int workedHours);
    }
}