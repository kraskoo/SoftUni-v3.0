namespace P08MilitaryElite.Interfaces
{
    using System.Collections.Generic;

    public interface IMilitaryRepository
    {
        IEnumerable<ISoldier> Privates { get; }

        void AddPrivate(ISoldier soldiers);
    }
}