namespace P08MilitaryElite.Interfaces
{
    using System.Collections.Generic;

    public interface ILeutenantGeneral : IPrivate
    {
        IEnumerable<IPrivate> Privates { get; }
    }
}