namespace P08MilitaryElite.Interfaces.Factories
{
    using System.Collections.Generic;

    public interface IFactoryMethod<out T> where T : ISoldier
    {
        IAbstractFactory<T> CreateFactory(
            string type, ICollection<ISoldier> soldiers, params object[] arguments);
    }
}