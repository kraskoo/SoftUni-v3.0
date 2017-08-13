namespace _02.Topological_Sorting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class SourceRemovalTopologicalSorting : ISortable
    {
        private readonly Dictionary<string, List<string>> graph;
        private readonly Dictionary<string, int> predecessorsCount;

        public SourceRemovalTopologicalSorting(Dictionary<string, List<string>> graph)
        {
            this.graph = graph;
            this.predecessorsCount = new Dictionary<string, int>();
        }

        public ICollection<string> TopSort()
        {
            this.GetPredecessors();
            this.graph.Values
                .SelectMany(v => v)
                .Where(v => !this.graph.ContainsKey(v))
                .Distinct()
                .Apply()
                .ForEachTerminal(str => this.graph.Add(str, new List<string>()));
            var sorted = new List<string>();
            while (true)
            {
                var nodeToRemove = predecessorsCount.Keys
                    .FirstOrDefault(x => predecessorsCount[x] == 0);
                if (nodeToRemove == null)
                {
                    break;
                }

                foreach (var childNode in this.graph[nodeToRemove])
                {
                    this.predecessorsCount[childNode]--;
                }

                predecessorsCount.Remove(nodeToRemove);
                graph.Remove(nodeToRemove);
                sorted.Add(nodeToRemove);
            }

            if (graph.Count > 0)
            {
                throw new InvalidOperationException();
            }

            return sorted;
        }

        private void GetPredecessors()
        {
            foreach (var node in this.graph)
            {
                if (!predecessorsCount.ContainsKey(node.Key))
                {
                    predecessorsCount.Add(node.Key, 0);
                }

                foreach (var childNode in node.Value)
                {
                    if (!predecessorsCount.ContainsKey(childNode))
                    {
                        predecessorsCount.Add(childNode, 0);
                    }

                    predecessorsCount[childNode]++;
                }
            }
        }
    }
}