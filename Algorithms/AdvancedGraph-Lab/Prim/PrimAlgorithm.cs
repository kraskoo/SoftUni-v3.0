namespace Prim
{
    using System.Collections.Generic;
    using System.Linq;

    public class PrimAlgorithm
    {
        public static List<Edge> Prim(List<Edge> edges)
        {
            var graph = BuildGraph(edges);
            var spanningTreeNodes = new HashSet<int>();
            var spanningTreeEdges = new List<Edge>();
            foreach (var startNode in graph.Keys)
            {
                if (!spanningTreeNodes.Contains(startNode))
                {
                    Prim(graph, spanningTreeNodes, startNode, spanningTreeEdges);
                }
            }

            return spanningTreeEdges
                .OrderBy(te => te.Weight)
                .ThenBy(te => te.StartNode)
                .ToList();
        }

        private static void Prim(
            Dictionary<int, List<Edge>> graph,
            ISet<int> spanningTreeNodes, int startNode,
            ICollection<Edge> spannngTreeEdges)
        {
            spanningTreeNodes.Add(startNode);
            var priorityQueue = new PriorityQueue<Edge>();
            foreach (var edge in graph[startNode])
            {
                priorityQueue.Enqueue(edge);
            }

            while (priorityQueue.Count > 0)
            {
                var smallestEdge = priorityQueue.ExtractMin();
                if (spanningTreeNodes.Contains(smallestEdge.StartNode) ^
                    spanningTreeNodes.Contains(smallestEdge.EndNode))
                {
                    var nonTreeNode = spanningTreeNodes.Contains(smallestEdge.StartNode) ?
                        smallestEdge.EndNode :
                        smallestEdge.StartNode;
                    spannngTreeEdges.Add(smallestEdge);
                    spanningTreeNodes.Add(nonTreeNode);
                    foreach (var newEdge in graph[nonTreeNode])
                    {
                        if (newEdge != smallestEdge)
                        {
                            priorityQueue.Enqueue(newEdge);
                        }
                    }
                }
            }
        }

        private static Dictionary<int, List<Edge>> BuildGraph(IEnumerable<Edge> edges)
        {
            var graph = new Dictionary<int, List<Edge>>();
            foreach (var edge in edges)
            {
                if (!graph.ContainsKey(edge.StartNode))
                {
                    graph.Add(edge.StartNode, new List<Edge>());
                }

                graph[edge.StartNode].Add(edge);
                if (!graph.ContainsKey(edge.EndNode))
                {
                    graph.Add(edge.EndNode, new List<Edge>());
                }

                graph[edge.EndNode].Add(edge);
            }

            return graph;
        }
    }
}