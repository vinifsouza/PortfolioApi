using Portfolio.Api.Model.Github.Project;
using Portfolio.Api.Facades;

using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("{userId}", Name = "GetRepositories")]
        public async Task<ActionResult<List<GithubProject>>> Get(string userId)
        {
            var response = await _facade.GetRepositories(userId);
            return Ok(response);
        }
    }
}
