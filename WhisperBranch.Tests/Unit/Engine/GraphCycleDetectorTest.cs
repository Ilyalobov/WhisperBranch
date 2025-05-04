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
        public void HasCycle_ReturnsTrue_WhenGraphHasCycle()
        {
            // Arrange
            var nodeA = new GraphDto { Id = 1 };
            var nodeB = new GraphDto { Id = 2 };
            var nodeC = new GraphDto { Id = 3 };

            nodeA.Children = new[] { nodeB };
            nodeB.Children = new[] { nodeC };
            nodeC.Children = new[] { nodeA }; // цикл A → B → C → A

            var detector = new GraphCycleDetector();

            // Act
            var result = detector.HasCycle(new[] { nodeA });

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void HasCycle_ReturnsFalse_WhenGraphIsAcyclic()
        {
            // Arrange
            var nodeA = new GraphDto { Id = 1 };
            var nodeB = new GraphDto { Id = 2 };
            var nodeC = new GraphDto { Id = 3 };

            nodeA.Children = new[] { nodeB };
            nodeB.Children = new[] { nodeC };
            nodeC.Children = Array.Empty<GraphDto>();

            var detector = new GraphCycleDetector();

            // Act
            var result = detector.HasCycle(new[] { nodeA });

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void HasCycle_ReturnsFalse_WhenGraphIsEmpty()
        {
            // Arrange
            var detector = new GraphCycleDetector();

            // Act
            var result = detector.HasCycle(Array.Empty<INode>());

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void HasCycle_ReturnsTrue_WhenDisconnectedGraphHasCycle()
        {
            // Arrange
            var nodeA = new GraphDto { Id = 1 };
            var nodeB = new GraphDto { Id = 2 };
            var nodeC = new GraphDto { Id = 3 };

            nodeA.Children = new[] { nodeB };
            nodeB.Children = new[] { nodeA }; 
            nodeC.Children = Array.Empty<GraphDto>();

            var detector = new GraphCycleDetector();

            // Act
            var result = detector.HasCycle(new[] { nodeA, nodeC });

            // Assert
            Assert.True(result);
        }
    }

}
