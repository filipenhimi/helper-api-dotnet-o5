using Newtonsoft.Json;

namespace helper_api_dotnet_o5.Models.G12Nba
{
    public partial class Team
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("conference")]
        public string? Conference { get; set; }

        [JsonProperty("full_name")]
        public string? FullName { get; set; }

        [JsonProperty("abbreviation")]
        public string? Abbreviation { get; set; }
    }
}

public enum Conference
{
    East,
    West,
}
