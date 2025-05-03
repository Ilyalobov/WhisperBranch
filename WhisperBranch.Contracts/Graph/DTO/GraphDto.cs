using WhisperBranch.Contracts.Interfaces;

namespace WhisperBranch.Contracts.Graph
{
    public class GraphDto : INode
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public IEnumerable<INode> Children { get; set; } = new List<INode>();
    }
}
