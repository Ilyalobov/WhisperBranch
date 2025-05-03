namespace WhisperBranch.Contracts.Interfaces
{
    public interface INode
    {
        public int Id { get; set; }

        public IEnumerable<INode> Children { get; set; }
    }
}
