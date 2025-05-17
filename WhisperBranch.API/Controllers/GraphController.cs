using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WhisperBranch.Application.Dispatcher.CommandHandling;
using WhisperBranch.Application.Dispatcher.QueryHandling;
using WhisperBranch.Application.Graph.Command;
using WhisperBranch.Contracts.Graph;

namespace WhisperBranch.API.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Components.Route("[controller]")]
    public class GraphController : ControllerBase
    {

        [HttpPost(nameof(CreateAsync))]
        public async Task<bool> CreateAsync([FromBody] GraphDto graph, CancellationToken cancellationToken)
        {
            return true;
        }

        [HttpPost(nameof(ReadAsync))]
        public async Task<bool> ReadAsync([Required] Guid Id, [FromServices] IQueryDispatcher dispatcher, CancellationToken cancellationToken)
        {
            //var result = await dispatcher.Dispatch(new GetGraphByIdQuery(id));
            return true;
        }

        [HttpPost(nameof(UpdateAsync))]
        public async Task<bool> UpdateAsync([Required] Guid Id, [Required] GraphDto graph, [FromServices] ICommandDispatcher dispatcher, CancellationToken cancellationToken)
        {
            var result = await dispatcher.Dispatch(new CreateGraphCommand() 
                                                                            { 
                                                                             Id=graph.Id,
                                                                             Children=graph.Children,
                                                                             Value=graph.Value
                                                                            });
            return true;
        }


        [HttpPost(nameof(DeleteAsync))]
        public async Task<bool> DeleteAsync([Required] Guid Id, CancellationToken cancellationToken)
        {
            return true;
        }
    }
}
