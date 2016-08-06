namespace P08MilitaryElite.Interfaces
{
    public interface ISpecialisedSoldier : IPrivate
    {
        string Corps { get; }

        bool CanExistSpecialisedSoldier();
    }
}