using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhisperBranch.Contracts.Graph;

namespace WhisperBranch.Application.Graph
{
    public class GraphValidator: AbstractValidator<GraphDto>
    {


        public GraphValidator()
        {
            RuleFor(x => x)
                .Must((r)=> new GraphCycleDetector().HasCycle(new[] { r })).WithMessage("TreeId должен быть положительным");

            
        }
    }

    public class GraphCycleDetector
    {
        public bool HasCycle(GraphDto[] nodes)
        {
            var visited = new HashSet<GraphDto>();
            var recursionStack = new HashSet<GraphDto>();

            foreach (var node in nodes)
            {
                if (DetectCycleDFS(node, visited, recursionStack))
                    return true;
            }

            return false;
        }

        private bool DetectCycleDFS(GraphDto node, HashSet<GraphDto> visited, HashSet<GraphDto> stack)
        {
            if (stack.Contains(node))
                return true;

            if (visited.Contains(node))
                return false;

            visited.Add(node);
            stack.Add(node);

            foreach (var child in node.Children)
            {
                if (DetectCycleDFS(child, visited, stack))
                    return true;
            }

            stack.Remove(node);
            return false;
        }
    }
}
