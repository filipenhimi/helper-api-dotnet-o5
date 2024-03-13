
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
    }
}
