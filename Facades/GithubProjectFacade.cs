using Portfolio.Api.Model.Github.Project;
using Portfolio.Api.Service.Github.Project;

using System.Threading.Tasks;
using RestEase;

namespace Portfolio.Api.Facades
{
    public class GithubFacade
    {
        public async Task<IEnumerable<GithubProject>> GetRepositories(string userId)
        {
            IGitHubApi api = RestClient.For<IGitHubApi>("https://api.github.com");
            IEnumerable<GithubProject> repo = await api.GetRepoAsync(userId);
            return repo;
        }
    }
}