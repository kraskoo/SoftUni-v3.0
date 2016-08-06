namespace P08MilitaryElite.Interfaces
{
    using System.Collections.Generic;

    public interface IEngineer : ISpecialisedSoldier
    {
        IEnumerable<IPart> Repairs { get; }
    }
}