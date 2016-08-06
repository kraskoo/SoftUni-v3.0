namespace P08MilitaryElite.Interfaces.Factories
{
    public interface IMissionFactory
    {
        IMission CreateMission(string codeName, string state);
    }
}