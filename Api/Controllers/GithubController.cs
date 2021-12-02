using Portfolio.Api.Model.Github.Repository;
using Portfolio.Api.Facades;

using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace PortfolioApi.Controllers
{

    [ApiController]
    [Route("api/repositories")]
    public class GithubController : ControllerBase
    {
        private readonly ILogger<GithubController> _logger;
        private GithubFacade _facade = new GithubFacade();

        public GithubController(ILogger<GithubController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{username}", Name = "GetRepositories")]
        [SwaggerOperation(
            Summary = "Search repositories within pattern conditions",
            Description = "Pattern instructions coming soon",
            OperationId = "GetRepositories",
            Tags = new[] { "Repository" }
        )]
        [SwaggerResponse(200, "At least one repository found", typeof(GithubRepository))]
        [SwaggerResponse(204, "No repository found")]
        public async Task<ActionResult<IEnumerable<GithubRepository>>> Get(
            [FromRoute(Name = "username"), SwaggerParameter("Github username", Required = true)] string username,
            [FromQuery(Name = "skill"), SwaggerParameter("Filter request by skill", Required = false)] string? skill = ""
        )
        {
            var response = await _facade.GetRepositories(username, skill);

            if (response.Count() != 0)
            {
                return Ok(response);
            }

            return NoContent();
        }
    }
}
