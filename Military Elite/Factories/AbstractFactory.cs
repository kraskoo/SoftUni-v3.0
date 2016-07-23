namespace P08MilitaryElite.Factories
{
    using Interfaces;
    using Interfaces.Factories;

    public abstract class AbstractFactory<T> : IAbstractFactory<T>
        where T : ISoldier
    {
        protected AbstractFactory(string id, string firstName, string lastName)
        {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        protected T Soldier { get; set; }

        protected string Id { get; }

        protected string FirstName { get; }

        protected string LastName { get; }

        public T Create()
        {
            return this.Soldier;
        }
    }
}