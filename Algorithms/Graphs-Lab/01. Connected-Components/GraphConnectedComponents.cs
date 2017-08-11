namespace _01.Connected_Components
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class GraphConnectedComponents
    {
        private static List<int>[] graph;
        private static List<bool> used;
        private static List<int> tempVerticesCollector;
        private static StringBuilder output;

        public static void Main()
        {
            Initialize();
            ReadGraph();
            FindGraphConnectedComponents();
            Print();
        }

        private static void Initialize()
        {
            tempVerticesCollector = new List<int>();
            output = new StringBuilder();
        }

        private static void Print()
        {
            Print(Console.Out);
        }

        private static void Print(TextWriter @out)
        {
            var outputResult = output.ToString().Trim();
            if (!string.IsNullOrEmpty(outputResult))
            {
                @out.WriteLine(outputResult);
            }
        }

        private static void ReadGraph()
        {
            graph = ReadGraph(Console.In);
            used = Enumerable.Repeat(false, graph.Length).ToList();
        }

        private static List<int>[] ReadGraph(TextReader @in)
        {
            int n = int.Parse(@in.ReadLine());
            var newGraph = new List<int>[n];
            for (int i = 0; i < n; i++)
            {
                newGraph[i] = @in.ReadLine()
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToList();
            }

            return newGraph;
        }

        private static void FindGraphConnectedComponents()
        {
            FindGraphConnectedComponents(output);
        }

        private static void FindGraphConnectedComponents(StringBuilder tempBuilder)
        {
            for (int i = 0; i < graph.Length; i++)
            {
                if (!used[i])
                {
                    DFS(i);
                    tempBuilder.AppendLine($"Connected component: {string.Join(" ", tempVerticesCollector)}");
                    tempVerticesCollector.Clear();
                }
            }
        }

        private static void DFS(int index)
        {
            if (!used[index])
            {
                used[index] = true;
                foreach (var vertex in graph[index])
                {
                    DFS(vertex);
                }

                tempVerticesCollector.Add(index);
            }
        }
    }
}