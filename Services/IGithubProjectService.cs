﻿using Portfolio.Api.Model.Github.Project;

using System.Threading.Tasks;
using RestEase;


namespace Portfolio.Api.Service.Github.Project
{
    [Header("User-Agent", "RestEase")]
    public interface IGitHubApi
    {

        [Get("users/{userId}/repos")]
        Task<IEnumerable<GithubProject>> GetRepoAsync([Path] string userId);

        [Get("{userId}/{repoName}/{branch}/README.md")]
        Task<string> GetReadmeContentAsync(
            [Path] string userId,
            [Path] string repoName,
            [Path] string branch
        );
    }
}
