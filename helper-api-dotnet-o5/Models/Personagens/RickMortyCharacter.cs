using Newtonsoft.Json;

namespace helper_api_dotnet_o5.Models.Personagens
{
    public class RickMortyCharacter
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("species")]
        public string Species { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("origin")]
        public Origin Origin { get; set; }

        [JsonProperty("location")]
        public Location Location { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("episode")]
        public List<string> Episode { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("created")]
        public DateTime Created { get; set; }
    }
    public class Location
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
    public class Info
    {
        [JsonProperty("count")]
        public int count { get; set; }

        [JsonProperty("pages")]
        public int pages { get; set; }

        [JsonProperty("next")]
        public string next { get; set; }

        [JsonProperty("prev")]
        public string prev { get; set; }
    }
    public class Origin
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
    public class RickMortyCharacterList
    {
        public Info Info { get; set; }
        public List<RickMortyCharacter> Results { get; set; }
    }
}