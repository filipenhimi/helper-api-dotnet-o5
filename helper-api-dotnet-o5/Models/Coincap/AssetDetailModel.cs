using Newtonsoft.Json;

namespace helper_api_dotnet_o5.Models.Coincap
{
    public partial class AssetDetail
    {
        [JsonProperty("id")]
        public PaiId Id { get; set; }

        [JsonProperty("rank")]
        public Rank Rank { get; set; }

        [JsonProperty("symbol")]
        public Symbol Symbol { get; set; }

        [JsonProperty("name")]
        public Name Name { get; set; }

        [JsonProperty("supply")]
        public Supply Supply { get; set; }

        [JsonProperty("maxSupply")]
        public MaxSupply MaxSupply { get; set; }

        [JsonProperty("marketCapUsd")]
        public MarketCapUsd MarketCapUsd { get; set; }

        [JsonProperty("volumeUsd24Hr")]
        public VolumeUsd24Hr VolumeUsd24Hr { get; set; }

        [JsonProperty("priceUsd")]
        public PriceUsd PriceUsd { get; set; }

        [JsonProperty("changePercent24Hr")]
        public ChangePercent24Hr ChangePercent24Hr { get; set; }

        [JsonProperty("vwap24Hr")]
        public Vwap24Hr Vwap24Hr { get; set; }

    }
}
