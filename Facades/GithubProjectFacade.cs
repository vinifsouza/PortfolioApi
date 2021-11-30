using Portfolio.Api.Model.Github.Project;
using Portfolio.Api.Service.Github.Project;

using System.Threading.Tasks;
using RestEase;
using System.Linq;

namespace Portfolio.Api.Facades
{
    public class GithubFacade
    {
        public async Task<IEnumerable<GithubProject>> GetRepositories(string userId)
        {
            IGitHubApi api = RestClient.For<IGitHubApi>("https://api.github.com");
            IEnumerable<GithubProject> repo = await api.GetRepoAsync(userId);

            var filter = _filterListablesProjectsToPortfolio(repo);

            return filter;
        }

        private IEnumerable<GithubProject> _filterListablesProjectsToPortfolio(IEnumerable<GithubProject> repositories)
        {
            return repositories
            .Where(r => r.Topics.Contains("portfolio-content"));
        }

        private async Task<string> _getInfosToDisplay(string userId, string repoName, string branch)
        {
            IGitHubApi apiDownload = RestClient.For<IGitHubApi>("https://raw.githubusercontent.com");

            string readmeContent = (await apiDownload.GetReadmeContentAsync(
                userId,
                repoName,
                branch
            ));

            return readmeContent;
        }
    }
}