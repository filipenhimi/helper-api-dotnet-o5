using Newtonsoft.Json;

namespace helper_api_dotnet_o5.Models.Coincap
{
    public partial class Assets
    {
        [JsonProperty("id")]
        public PaiId Id { get; set; }

        [JsonProperty("symbol")]
        public Symbol Symbol { get; set; }

        [JsonProperty("name")]
        public Name Name { get; set; }

        [JsonProperty("supply")]
        public Supply Supply { get; set; }

    }
}
