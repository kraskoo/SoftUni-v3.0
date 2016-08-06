namespace P08MilitaryElite.Factories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces;
    using Interfaces.Factories;

    public class FactoryMethod<T> : IFactoryMethod<T> where T : ISoldier
    {
        private ICollection<IPrivate> currentPrivates;

        public IAbstractFactory<T> CreateFactory(
            string type, ICollection<ISoldier> soldiers, params object[] arguments)
        {
            if (soldiers != null)
            {
                this.currentPrivates = soldiers.OfType<IPrivate>().ToArray();
            }

            switch (type)
            {
                case "Private":
                    return this.CreatePrivateFactory(arguments);
                case "Commando":
                    return this.CreateCommandoFactory(arguments);
                case "Engineer":
                    return this.CreateEngineerFactory(arguments);
                case "LeutenantGeneral":
                    return this.CreateLeutenantGeneralFactory(arguments);
                case "Spy":
                    return this.CreateSpyFactory(arguments);
                default:
                    throw new ArgumentException("Unknown type.");
            }
        }

        private IAbstractFactory<T> CreatePrivateFactory(object[] arguments)
        {
            return
                new PrivateFactory(
                    arguments[0].ToString(),
                    arguments[1].ToString(),
                    arguments[2].ToString(),
                    double.Parse(arguments[3].ToString())) as IAbstractFactory<T>;
        }

        private IAbstractFactory<T> CreateCommandoFactory(object[] arguments)
        {
            var missionCollection = arguments
                .Skip(5).Take(arguments.Length - 5)
                .Select(a => a.ToString()).ToArray();
            return
                new CommandoFactory(
                    arguments[0].ToString(),
                    arguments[1].ToString(),
                    arguments[2].ToString(),
                    double.Parse(arguments[3].ToString()),
                    arguments[4].ToString(),
                    missionCollection) as IAbstractFactory<T>;
        }

        private IAbstractFactory<T> CreateEngineerFactory(object[] arguments)
        {
            var partCollection = arguments
                .Skip(5).Take(arguments.Length - 5)
                .Select(a => a.ToString()).ToArray();
            return
                new EngineerFactory(
                    arguments[0].ToString(),
                    arguments[1].ToString(),
                    arguments[2].ToString(),
                    double.Parse(arguments[3].ToString()),
                    arguments[4].ToString(),
                    partCollection) as IAbstractFactory<T>;
        }

        private IAbstractFactory<T> CreateLeutenantGeneralFactory(object[] arguments)
        {
            var privateCollection = arguments
                .Skip(4).Take(arguments.Length - 4)
                .Select(a => a.ToString()).ToArray();
            return
                new LeutenantGeneralFactory(
                    arguments[0].ToString(),
                    arguments[1].ToString(),
                    arguments[2].ToString(),
                    double.Parse(arguments[3].ToString()),
                    this.currentPrivates,
                    privateCollection) as IAbstractFactory<T>;
        }

        private IAbstractFactory<T> CreateSpyFactory(object[] arguments)
        {
            return
                new SpyFactory(
                    arguments[0].ToString(),
                    arguments[1].ToString(),
                    arguments[2].ToString(),
                    int.Parse(arguments[3].ToString())) as IAbstractFactory<T>;
        }
    }
}