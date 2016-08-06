namespace P08MilitaryElite.Interfaces.Factories
{
    public interface IAbstractFactory<out T> where T : ISoldier
    {
        T Create();
    }
}