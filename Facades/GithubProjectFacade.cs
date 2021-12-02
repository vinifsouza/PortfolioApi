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
            IEnumerable<GithubProject> repositories = await api.GetRepoAsync(userId);

            var filter = _filterListablesProjectsToPortfolio(repositories);

            for (int idx = 0; idx < filter.Count(); idx++)
            {
                var repo = filter.ElementAt(idx);
                var readme = await _getReadmeAsync(userId, repo.Name, repo.Branch);
                repo.Icons = _getIconsFromReadme(readme);
                repo.Logo = _getLogoFromReadme(readme);
                repo.Title = _getTitle(readme);
            }

            return filter;
        }

        private IEnumerable<GithubProject> _filterListablesProjectsToPortfolio(IEnumerable<GithubProject> repositories)
        {
            return repositories
            .Where(r => r.Topics!.Contains("portfolio-content"));
        }

        private async Task<string> _getReadmeAsync(string userId, string repoName, string branch)
        {
            IGitHubApi apiDownload = RestClient.For<IGitHubApi>("https://raw.githubusercontent.com");

            var readmeContent = await apiDownload.GetReadmeContentAsync(
                userId,
                repoName,
                branch
            );

            return readmeContent;
        }

        private IEnumerable<string> _getIconsFromReadme(string readme)
        {
            const string TARGET_FLAG = "<iconfy>";
            const string INITIALIZER_FLAG = "icon->";

            var splitByTargetFlag = readme.Split(TARGET_FLAG);
            var filterByInitializer = splitByTargetFlag.Where(i => i.Contains(INITIALIZER_FLAG));
            var mappedIcons = filterByInitializer.Select(w => w.Substring(INITIALIZER_FLAG.Length));

            return mappedIcons;
        }

        private string _getLogoFromReadme(string readme)
        {
            const string TARGET_FLAG = "<logo-url>";
            const string INITIALIZER_FLAG = "logo_url->";

            var splitByTargetFlag = readme.Split(TARGET_FLAG);
            var filterByInitializer = splitByTargetFlag.Where(i => i.Contains(INITIALIZER_FLAG));
            var logoUrl = filterByInitializer.Select(w => w.Substring(TARGET_FLAG.Length)).ElementAt(0);

            return logoUrl;
        }

        private string _getTitle(string readme)
        {
            const string INITIALIZER_FLAG = "# ";

            string splitByBreakline = readme.Split('\n').ElementAt(0);

            int startPosition = splitByBreakline.IndexOf(INITIALIZER_FLAG) + 2;
            string subWithStart = splitByBreakline.Substring(startPosition);

            return subWithStart;
        }
    }
}
