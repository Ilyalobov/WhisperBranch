using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhisperBranch.Application.Dispatcher.CommandHandling;
using WhisperBranch.Contracts.Interfaces;

namespace WhisperBranch.Application.Graph.Command
{
    public class CreateGraphCommand : ICommand<Guid>
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public IEnumerable<INode> Children { get; set; } = new List<INode>();
    }
}
