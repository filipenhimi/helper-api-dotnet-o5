using Newtonsoft.Json;

namespace helper_api_dotnet_o5.Models.G9Pokedex;

public class G9PokemonSpecieResultDto
{
    [JsonProperty("evolution_chain")]
    public EvolutionChain EvolutionChain { get; set; }
}

public partial class EvolutionChain
{
    [JsonProperty("url")]
    public string Url { get; set; }
}