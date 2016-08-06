namespace P08MilitaryElite.Interfaces
{
    using System.Collections.Generic;

    public interface ICommando : ISpecialisedSoldier
    {
        IEnumerable<IMission> Missions { get; }
    }
}