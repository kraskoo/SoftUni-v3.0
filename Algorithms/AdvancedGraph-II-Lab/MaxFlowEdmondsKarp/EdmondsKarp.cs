namespace MaxFlowEdmonds_Karp
{
    using System.Collections.Generic;

    public class EdmondsKarp
    {
        private static int[][] graph;
        private static int[] parent;

        public static int FindMaxFlow(int[][] targetGraph)
        {
            graph = targetGraph;
            parent = new int[graph.Length];
            for (int i = 0; i < graph.Length; i++)
            {
                parent[i] = -1;
            }

            int maxFlow = 0;
            int start = 0;
            int end = graph.Length - 1;
            while (BreadFirstSearch(start, end))
            {
                int pathFlow = int.MaxValue;
                int currentNode = end;
                while (currentNode != start)
                {
                    int previousNode = parent[currentNode];
                    if (pathFlow > graph[previousNode][currentNode])
                    {
                        pathFlow = graph[previousNode][currentNode];
                    }

                    currentNode = parent[currentNode];
                }

                maxFlow += pathFlow;
                currentNode = end;
                while (currentNode != start)
                {
                    int previousNode = parent[currentNode];
                    graph[previousNode][currentNode] -= pathFlow;
                    graph[currentNode][previousNode] += pathFlow;
                    currentNode = previousNode;
                }
            }

            return maxFlow;
        }

        private static bool BreadFirstSearch(int start, int end)
        {
            bool[] visited = new bool[graph.Length];
            var queue = new Queue<int>();
            queue.Enqueue(start);
            visited[start] = true;
            while (queue.Count > 0)
            {
                var currentNode = queue.Dequeue();
                for (int i = start; i < graph.Length; i++)
                {
                    if (!visited[i] && graph[currentNode][i] != 0)
                    {
                        queue.Enqueue(i);
                        parent[i] = currentNode;
                        visited[i] = true;
                    }
                }
            }

            return visited[end];
        }
    }
}