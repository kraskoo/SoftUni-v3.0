namespace P10InfernoInfinity.Interfaces
{
    using System.Collections.Generic;

    public interface IGemSocketable : ISocketable, IAddableGem, IRemovableGem
    {
        IEnumerable<IGem> Gems { get; }
    }
}