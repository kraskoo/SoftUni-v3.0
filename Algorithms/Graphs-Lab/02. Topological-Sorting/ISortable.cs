namespace _02.Topological_Sorting
{
    using System.Collections.Generic;

    public interface ISortable
    {
        ICollection<string> TopSort();
    }
}