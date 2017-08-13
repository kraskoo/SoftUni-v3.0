namespace _02.Topological_Sorting
{
    using System.Collections.Generic;
    using System.Linq;

    public class TopologicalSorter
    {
        private readonly Dictionary<string, List<string>> graph;
        private readonly ISortable sortable;

        public TopologicalSorter(Dictionary<string, List<string>> graph, ISortable sortable)
        {
            this.graph = graph.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            this.sortable = sortable;
        }

        public TopologicalSorter(
            Dictionary<string, List<string>> graph) : this(
                graph,
                new SourceRemovalTopologicalSorting(graph))
        {
        }


        public ICollection<string> TopSort()
        {
            return this.sortable.TopSort();
        }
    }
}