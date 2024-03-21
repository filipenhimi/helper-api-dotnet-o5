using System.Collections.Generic;
using Newtonsoft.Json;

namespace helper_api_dotnet_o5.Models.Musicas
{
    public class MusicResponse
    {
        [JsonProperty("response")]
        public MusicResponseData Response { get; set; }
    }

    public class MusicResponseData
    {
        [JsonProperty("numFound")]
        public int NumFound { get; set; }

        [JsonProperty("start")]
        public int Start { get; set; }

        [JsonProperty("numFoundExact")]
        public bool NumFoundExact { get; set; }

        [JsonProperty("docs")]
        public List<Musica> Docs { get; set; }
    }

    public class Musica
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("langID")]
        public int LangID { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("band")]
        public string Band { get; set; }
    }
}
