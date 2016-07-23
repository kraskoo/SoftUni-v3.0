namespace P08MilitaryElite.Factories
{
    using Interfaces;
    using Interfaces.Factories;
    using Models;

    public class SpyFactory : AbstractFactory<ISpy>, ISpyFactory
    {
        public SpyFactory(
            string id,
            string firstName,
            string lastName,
            int codeName) : base(id, firstName, lastName)
        {
            this.Soldier = new Spy(id, firstName, lastName, codeName);
        }
    }
}