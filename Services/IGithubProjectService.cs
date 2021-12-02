using Portfolio.Api.Model.Github.Repository;

using System.Threading.Tasks;
using RestEase;


namespace Portfolio.Api.Service.Github.Repository
{
    [Header("User-Agent", "RestEase")]
    public interface IGitHubApi
    {

        [Get("users/{userId}/repos")]
        Task<IEnumerable<GithubRepository>> GetRepoAsync([Path] string userId);

        [Get("{userId}/{repoName}/{branch}/README.md")]
        Task<string> GetReadmeContentAsync(
            [Path] string userId,
            [Path] string repoName,
            [Path] string branch
        );
    }
}
