namespace P08MilitaryElite.Factories
{
    using System.Collections.Generic;
    using Interfaces;
    using Interfaces.Factories;
    using Models;

    public class LeutenantGeneralFactory
        : AbstractFactory<ILeutenantGeneral>, ILeutenantGeneralFactory
    {
        public LeutenantGeneralFactory(
            string id,
            string firstName,
            string lastName,
            double salary,
            ICollection<IPrivate> privates,
            params string[] ids) : base(id, firstName, lastName)
        {
            this.Soldier =
                new LeutenantGeneral(id, firstName, lastName, salary, privates, ids);
        }
    }
}