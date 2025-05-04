#region usings

using WhisperBranch.Contracts.Graph;
using WhisperBranch.Contracts.Interfaces;
using WhisperBranch.TreeEngine.Algorithms;

#endregion


namespace WhisperBranch.Tests.Unit.Engine
{
  

    public class GraphCycleDetectorTests
    {
        [Fact]
        public void DetectsCycle_WhenGraphHasCycle_ReturnsTrue()
        {
            // A -> B -> C -> A (цикл)
            var nodeA = new GraphDto { Id = 1 };
            var nodeB = new GraphDto { Id = 2 };
            var nodeC = new GraphDto { Id = 3 };

            nodeA.Children = new[] { nodeB };
            nodeB.Children = new[] { nodeC };
            nodeC.Children = new[] { nodeA }; 

            var detector = new GraphCycleDetector();
            var hasCycle = detector.HasCycle(new[] { nodeA });

            Assert.True(hasCycle);
        }

        [Fact]
        public void DetectsCycle_WhenGraphIsAcyclic_ReturnsFalse()
        {
            // A -> B -> C
            var nodeA = new GraphDto { Id = 1 };
            var nodeB = new GraphDto { Id = 2 };
            var nodeC = new GraphDto { Id = 3 };

            nodeA.Children = new[] { nodeB };
            nodeB.Children = new[] { nodeC };
            nodeC.Children = Array.Empty<GraphDto>();

            var detector = new GraphCycleDetector();
            var hasCycle = detector.HasCycle(new[] { nodeA });

            Assert.False(hasCycle);
        }

        [Fact]
        public void DetectsCycle_WhenGraphIsEmpty_ReturnsFalse()
        {
            var detector = new GraphCycleDetector();
            var hasCycle = detector.HasCycle(Array.Empty<INode>());

            Assert.False(hasCycle);
        }

        [Fact]
        public void DetectsCycle_WhenDisconnectedGraphHasCycle_ReturnsTrue()
        {
            // Граф: A -> B -> A (цикл), и отдельно C
            var nodeA = new GraphDto { Id = 1 };
            var nodeB = new GraphDto { Id = 2 };
            var nodeC = new GraphDto { Id = 3 };

            nodeA.Children = new[] { nodeB };
            nodeB.Children = new[] { nodeA }; 
            nodeC.Children = Array.Empty<GraphDto>(); 

            var detector = new GraphCycleDetector();
            var hasCycle = detector.HasCycle(new[] { nodeA, nodeC });

            Assert.True(hasCycle);
        }
    }

}
