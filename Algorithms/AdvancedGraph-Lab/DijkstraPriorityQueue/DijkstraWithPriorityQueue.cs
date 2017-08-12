namespace Dijkstra
{
    using System.Collections.Generic;

    public static class DijkstraWithPriorityQueue
    {
        public static List<int> DijkstraAlgorithm(
            Dictionary<Node, Dictionary<Node, int>> graph,
            Node sourceNode,
            Node destinationNode)
        {
            int?[] previous = new int?[graph.Count];
            bool[] visited = new bool[graph.Count];
            var priorityQueue = new PriorityQueue<Node>();
            foreach (var pair in graph)
            {
                pair.Key.DistanceFromStart = double.PositiveInfinity;
            }

            sourceNode.DistanceFromStart = 0;
            priorityQueue.Enqueue(sourceNode);
            while (priorityQueue.Count > 0)
            {
                var smallest = priorityQueue.ExtractMin();
                if (smallest.Equals(destinationNode))
                {
                    break;
                }

                foreach (var edge in graph[smallest])
                {
                    if (!visited[edge.Key.Id])
                    {
                        priorityQueue.Enqueue(edge.Key);
                        visited[edge.Key.Id] = true;
                    }

                    var newDistance = smallest.DistanceFromStart + edge.Value;
                    if (newDistance < edge.Key.DistanceFromStart)
                    {
                        edge.Key.DistanceFromStart = newDistance;
                        previous[edge.Key.Id] = smallest.Id;
                        priorityQueue.DecreaseKey(edge.Key);
                    }
                }
            }

            if (double.IsInfinity(destinationNode.DistanceFromStart))
            {
                return null;
            }

            var path = new List<int>();
            int? currentNode = destinationNode.Id;
            while (currentNode != null)
            {
                path.Add(currentNode.Value);
                currentNode = previous[currentNode.Value];
            }

            path.Reverse();
            return path;
        }
    }
}