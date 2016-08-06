namespace P08MilitaryElite.Factories
{
    using Interfaces;
    using Interfaces.Factories;
    using Models;

    public class CommandoFactory : AbstractFactory<ICommando>, ICommandoFactory
    {
        public CommandoFactory(
            string id,
            string firstName,
            string lastName,
            double salary,
            string corps,
            params string[] missionsData) : base(id, firstName, lastName)
        {
            this.Soldier =
                new Commando(id, firstName, lastName, salary, corps, missionsData);
        }
    }
}