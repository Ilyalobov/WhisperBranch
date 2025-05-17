using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhisperBranch.Application.Dispatcher.CommandHandling;
using WhisperBranch.Application.Graph.Command;
using WhisperBranch.Domain.Interfaces;

namespace WhisperBranch.Application.Graph
{
    internal class CreateGraphCommandHandler : ICommandHandler<CreateGraphCommand, int>
    {
        private readonly IRepository<Graph> _graphRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateGraphCommandHandler(IRepository<Graph> graphRepository, IUnitOfWork unitOfWork)
        {
            _graphRepository = graphRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateGraphCommand command, CancellationToken cancellationToken)
        {
            var graph = new Graph(command.Name);

            var nodes = command.Nodes.ToDictionary(n => n.Id, n => new Node(n.Id));
            foreach (var dto in command.Nodes)
            {
                foreach (var childId in dto.ChildrenIds)
                {
                    nodes[dto.Id].AddChild(nodes[childId]);
                }
            }

            foreach (var node in nodes.Values)
            {
                graph.AddNode(node);
            }

            await _graphRepository.AddAsync(graph, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return graph.Id;
        }
    }
}