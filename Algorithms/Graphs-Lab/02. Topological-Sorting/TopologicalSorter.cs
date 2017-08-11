namespace _02.Topological_Sorting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class TopologicalSorter
    {
        private readonly Dictionary<string, List<string>> graph;
        private readonly HashSet<string> visited;
        private readonly HashSet<string> cycledNodes;

        public TopologicalSorter(Dictionary<string, List<string>> graph)
        {
            this.graph = graph.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            this.visited = new HashSet<string>();
            this.cycledNodes = new HashSet<string>();
        }
    
        public ICollection<string> TopSort()
        {
            var sorted = new LinkedList<string>();
            this.graph.Values
                .SelectMany(v => v)
                .Where(v => !this.graph.ContainsKey(v))
                .Distinct()
                .ForEachTerminal(str => this.graph.Add(str, new List<string>()));
            foreach (var vertex in this.graph.Keys)
            {
                this.DFS(vertex, sorted);
            }

            return sorted;
        }

        private void DFS(string vertex, LinkedList<string> sorted)
        {
            if (!visited.Contains(vertex))
            {
                visited.Add(vertex);
                cycledNodes.Add(vertex);
                foreach (var child in this.graph[vertex])
                {
                    this.DFS(child, sorted);
                }

                cycledNodes.Remove(vertex);
                sorted.AddFirst(vertex);
            }

            if (cycledNodes.Contains(vertex))
            {
                throw new InvalidOperationException();
            }
        }
    }
}