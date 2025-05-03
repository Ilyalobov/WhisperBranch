namespace WhisperBranch.Contracts.Graph
{
    public class GraphDto
    {
        public int Value { get; set; }

        public required GraphDto[] Children { get; set; }
    }
}
