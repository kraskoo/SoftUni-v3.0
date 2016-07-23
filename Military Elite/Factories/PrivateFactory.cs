namespace P08MilitaryElite.Factories
{
    using Interfaces;
    using Interfaces.Factories;
    using Models;

    public class PrivateFactory : AbstractFactory<IPrivate>, IPrivateFactory
    {
        public PrivateFactory(
            string id,
            string firstName,
            string lastName,
            double salary) : base(id, firstName, lastName)
        {
            this.Soldier = new Private(id, firstName, lastName, salary);
        }
    }
}