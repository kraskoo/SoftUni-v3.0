namespace P08MilitaryElite.Factories
{
    using Interfaces;
    using Interfaces.Factories;
    using Models;

    public class EngineerFactory : AbstractFactory<IEngineer>, IEngineerFactory
    {
        public EngineerFactory(
            string id,
            string firstName,
            string lastName,
            double salary,
            string corps,
            params string[] partsData) : base(id, firstName, lastName)
        {
            this.Soldier =
                new Engineer(id, firstName, lastName, salary, corps, partsData);
        }
    }
}