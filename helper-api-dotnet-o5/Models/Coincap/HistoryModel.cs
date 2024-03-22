using Newtonsoft.Json;

namespace helper_api_dotnet_o5.Models.Coincap
{

    public partial class History
    {
        [JsonProperty("data")]
        public List<AssetHistoryData> data { get; set; }
    }
    public partial class AssetHistoryData
    {
        [JsonProperty("priceUsd")]
        public string priceUsd { get; set; }

        [JsonProperty("time")]
        public string time { get; set; }

        [JsonProperty("date")]
        public string date { get; set; }
    }
}
