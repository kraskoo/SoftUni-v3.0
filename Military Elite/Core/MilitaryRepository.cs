namespace P08MilitaryElite.Core
{
    using System.Collections.Generic;
    using Interfaces;

    public class MilitaryRepository : IMilitaryRepository
    {
        private readonly IList<ISoldier> privates;

        public MilitaryRepository(IList<ISoldier> privates)
        {
            this.privates = privates;
        }

        public MilitaryRepository() : this(new List<ISoldier>())
        {
        }

        public IEnumerable<ISoldier> Privates => this.privates;

        public void AddPrivate(ISoldier soldier)
        {
            this.privates.Add(soldier);
        }
    }
}