using Newtonsoft.Json;

namespace helper_api_dotnet_o5.Models.G9Pokedex;

public class G9PokemonResult
{
    [JsonProperty("count")]
    public int Count { get; set; }

    [JsonProperty("next")]
    public string Next { get; set; }

    [JsonProperty("previous")]
    public string Previous { get; set; }

    [JsonProperty("results")]
    public List<ResultRequest> Results { get; set; }
}

public partial class ResultRequest
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("url")]
    public string Url { get; set; }
}