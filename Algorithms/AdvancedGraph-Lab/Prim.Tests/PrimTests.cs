namespace Prim.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PrimTests
    {
        [TestMethod]
        public void TestPrimWithSingleEdge()
        {
            var graphEdges = new List<Edge>
            {
                new Edge(0, 1, 3)
            };

            var minimumSpanningForest = PrimAlgorithm.Prim(graphEdges.ToList());
            var totalWeight = minimumSpanningForest.Sum(edge => edge.Weight);

            var expectedTotalWeight = 3;
            var expectedForest = new[] { graphEdges[0] };

            Assert.AreEqual(expectedTotalWeight, totalWeight, "Weights should match.");
            CollectionAssert.AreEqual(
                expectedForest,
                minimumSpanningForest,
                "The correct edges should be present in the MST in the correct order.");
        }

        [TestMethod]
        public void TestPrimWithTwoConnectedEdges()
        {
            var graphEdges = new List<Edge>
            {
                new Edge(0, 1, 3),
                new Edge(2, 1, 4)
            };

            var minimumSpanningForest = PrimAlgorithm.Prim(graphEdges.ToList());
            var totalWeight = minimumSpanningForest.Sum(edge => edge.Weight);

            var expectedTotalWeight = 7;
            var expectedForest = new[] { graphEdges[0], graphEdges[1] };

            Assert.AreEqual(expectedTotalWeight, totalWeight, "Weights should match.");
            CollectionAssert.AreEqual(
                expectedForest,
                minimumSpanningForest,
                "The correct edges should be present in the MST in the correct order.");
        }

        [TestMethod]
        public void TestPrimWithTwoEdgesFormingForest()
        {
            var graphEdges = new List<Edge>
            {
                new Edge(0, 1, 3),
                new Edge(2, 3, 4)
            };

            var minimumSpanningForest = PrimAlgorithm.Prim(graphEdges.ToList());
            var totalWeight = minimumSpanningForest.Sum(edge => edge.Weight);

            var expectedTotalWeight = 7;
            var expectedForest = new[] { graphEdges[0], graphEdges[1] };

            Assert.AreEqual(expectedTotalWeight, totalWeight, "Weights should match.");
            CollectionAssert.AreEqual(
                expectedForest,
                minimumSpanningForest,
                "The correct edges should be present in the MST in the correct order.");
        }

        [TestMethod]
        public void TestPrimWith9VerticesAnd11Edges()
        {
            var graphEdges = new List<Edge>
            {
                new Edge(0, 3, 9),
                new Edge(0, 5, 4),
                new Edge(0, 8, 5),
                new Edge(1, 4, 8),
                new Edge(1, 7, 7),
                new Edge(2, 6, 12),
                new Edge(3, 5, 2),
                new Edge(3, 6, 8),
                new Edge(3, 8, 20),
                new Edge(4, 7, 10),
                new Edge(6, 8, 7)
            };

            var minimumSpanningForest = PrimAlgorithm.Prim(graphEdges.ToList());
            var totalWeight = minimumSpanningForest.Sum(edge => edge.Weight);

            var expectedTotalWeight = 45;
            var expectedForest = new[]
            {
                graphEdges[6],
                graphEdges[1],
                graphEdges[2],
                graphEdges[4],
                graphEdges[10],
                graphEdges[3],
                graphEdges[5]
            };

            Assert.AreEqual(expectedTotalWeight, totalWeight, "Weights should match.");
            CollectionAssert.AreEqual(
                expectedForest,
                minimumSpanningForest,
                "The correct edges should be present in the MST in the correct order.");
        }

        [TestMethod]
        public void TestPrimWith9VerticesAnd10Edges()
        {
            var graphEdges = new List<Edge>
            {
                new Edge(0, 3, 9),
                new Edge(0, 8, 5),
                new Edge(1, 4, 8),
                new Edge(1, 7, 7),
                new Edge(2, 6, 12),
                new Edge(3, 5, 2),
                new Edge(3, 6, 8),
                new Edge(3, 8, 20),
                new Edge(4, 7, 10),
                new Edge(6, 8, 7)
            };

            var minimumSpanningForest = PrimAlgorithm.Prim(graphEdges.ToList());
            var totalWeight = minimumSpanningForest.Sum(edge => edge.Weight);

            var expectedTotalWeight = 49;
            var expectedForest = new[]
            {
                graphEdges[5],
                graphEdges[1],
                graphEdges[3],
                graphEdges[9],
                graphEdges[2],
                graphEdges[6],
                graphEdges[4]
            };

            Assert.AreEqual(expectedTotalWeight, totalWeight, "Weights should match.");
            CollectionAssert.AreEqual(
                expectedForest,
                minimumSpanningForest,
                "The correct edges should be present in the MST in the correct order.");
        }
    }
}
