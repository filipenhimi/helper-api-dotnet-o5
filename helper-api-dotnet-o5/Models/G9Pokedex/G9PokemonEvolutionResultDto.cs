using Newtonsoft.Json;

namespace helper_api_dotnet_o5.Models.G9Pokedex;

    public class G9PokemonEvolutionResultDto
    {
        [JsonProperty("chain")]
        public Chain Chain { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }
    }

    public partial class Chain
    {
        [JsonProperty("evolves_to")]
        public List<Chain> EvolvesTo { get; set; } = new List<Chain>();

        [JsonProperty("is_baby")]
        public bool IsBaby { get; set; }

        [JsonProperty("species")]
        public Species Species { get; set; }
    }

    public partial class Species
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }