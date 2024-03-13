using Newtonsoft.Json;

namespace helper_api_dotnet_o5.Models.Bancos
{
    public class Banco
    {
        [JsonProperty("ispb")]
        public string Ispb { get; set; }

        [JsonProperty("name")]
        public string Nome { get; set; }

        [JsonProperty("code")]
        public int? Codigo { get; set; }

        [JsonProperty("fullName")]
        public string NomeCompleto { get; set; }
    }
}
