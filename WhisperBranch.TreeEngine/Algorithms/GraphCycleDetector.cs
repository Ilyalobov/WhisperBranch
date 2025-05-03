using WhisperBranch.Contracts.Interfaces;

namespace WhisperBranch.TreeEngine.Algorithms
{
    public class GraphCycleDetector
    {
            public bool HasCycle(INode[] nodes)
            {
                var visited = new HashSet<int>();
                var recursionStack = new HashSet<int>();

                foreach (var node in nodes)
                {
                    if (DetectCycleDFS(node, visited, recursionStack))
                        return true;
                }

                return false;
            }

            private bool DetectCycleDFS(INode node, HashSet<int> visited, HashSet<int> stack)
            {
                if (stack.Contains(node.Id))
                    return true;

                if (visited.Contains(node.Id))
                    return false;

                visited.Add(node.Id);
                stack.Add(node.Id);

                foreach (var child in node.Children)
                {
                    if (DetectCycleDFS(child, visited, stack))
                        return true;
                }

                stack.Remove(node.Id);
                return false;
            }
        
    }
}
