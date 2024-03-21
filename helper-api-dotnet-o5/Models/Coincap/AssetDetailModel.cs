
using Newtonsoft.Json;
namespace helper_api_dotnet_o5.Models.Coincap
{

    public partial class AssetDetail
    {
        [JsonProperty("data")]
        public AssetDetailData data { get; set; }
    }
    public class AssetDetailData
    {
        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("symbol")]
        public string symbol { get; set; }

        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("supply")]
        public string supply { get; set; }

        [JsonProperty("maxSupply")]
        public string maxSupply { get; set; }

        [JsonProperty("marketCapUsd")]
        public string marketCap { get; set; }

        [JsonProperty("volumeUsd24Hr")]
        public string volumeUsd { get; set; }

        [JsonProperty("priceUsd")]
        public string priceUsd { get; set; }

        [JsonProperty("changePercent24Hr")]
        public string changePercent { get; set; }

        [JsonProperty("vwap24Hr")]
        public string averagePrice { get; set; }
    }
}
