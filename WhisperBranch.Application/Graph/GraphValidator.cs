using FluentValidation;
using WhisperBranch.Contracts.Graph;
using WhisperBranch.TreeEngine.Algorithms;

namespace WhisperBranch.Application.Graph
{
    public class GraphValidator: AbstractValidator<GraphDto>
    {
        public GraphValidator()
        {
            RuleFor(x => x)
                .Must((r)=> new GraphCycleDetector().HasCycle(new[]{ r }))
                .WithMessage("The graph has cyclic links.");
        }
    }
}
