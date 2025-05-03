using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WhisperBranch.Contracts.Graph;

namespace WhisperBranch.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GraphController : ControllerBase
    {
        [HttpPost(nameof(CreateAsync))]
        public async Task<bool> CreateAsync([Required] GraphDto graph, CancellationToken cancellationToken)
        {
            return true;
        }

        [HttpPost(nameof(ReadAsync))]
        public async Task<bool> ReadAsync([Required] Guid Id, CancellationToken cancellationToken)
        {
            return true;
        }

        [HttpPost(nameof(UpdateAsync))]
        public async Task<bool> UpdateAsync([Required] Guid Id, [Required] GraphDto graph, CancellationToken cancellationToken)
        {
            return true;
        }


        [HttpPost(nameof(DeleteAsync))]
        public async Task<bool> DeleteAsync([Required] Guid Id, CancellationToken cancellationToken)
        {
            return true;
        }
    }
}
