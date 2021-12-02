using Newtonsoft.Json;

namespace Portfolio.Api.Model.Github.Project
{
    public class GithubProject
    {
        [JsonProperty("name")]
        public string Name { get; set; } = "";

        public string Title { get; set; } = "";

        [JsonProperty("default_branch")]
        public string Branch { get; set; } = "";

        [JsonProperty("homepage")]
        public string? Homepage { get; set; }

        [JsonProperty("html_url")]
        public string? Url { get; set; }

        [JsonProperty("description")]
        public string? Description { get; set; }

        [JsonProperty("topics")]
        public IEnumerable<string>? Topics { get; set; }

        public IEnumerable<string>? Icons { get; set; }
        public string Logo { get; set; } = "";
    }
}