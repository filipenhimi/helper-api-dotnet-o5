using Newtonsoft.Json;

namespace helper_api_dotnet_o5.Models.Moedas
{
    public class Currency
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("high")]
        public string High { get; set; }

        [JsonProperty("low")]
        public string Low { get; set; }

        [JsonProperty("bid")]
        public string Bid { get; set; }

        [JsonProperty("ask")]
        public string Ask { get; set; }

        [JsonProperty("create_date")]
        public string CreateDate { get; set; }

        // Construtor para inicializar propriedades
        public Currency()
        {
            Code = string.Empty;
            Name = string.Empty;
            High = string.Empty;
            Low = string.Empty;
            Bid = string.Empty;
            Ask = string.Empty;
            CreateDate = string.Empty;
        }
    }
}