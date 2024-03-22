using Newtonsoft.Json;

namespace helper_api_dotnet_o5.Models.Coincap
{
    public class Assets
    {
        [JsonProperty("data")]
        public List<AssetDetailData> data { get; set; }
    }
}
