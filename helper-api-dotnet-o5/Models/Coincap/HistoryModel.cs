using Newtonsoft.Json;

namespace helper_api_dotnet_o5.Models.Coincap
{
    public partial class History
    {

        [JsonProperty("priceUsd")]
        public PaiId PaiId { get; set; }

        [JsonProperty("time")]
        public Time Time { get; set; }

        [JsonProperty("date")]
        public Date Date { get; set; }

    }
}