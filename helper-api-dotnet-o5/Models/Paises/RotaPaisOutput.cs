using Newtonsoft.Json;

namespace helper_api_dotnet_o5.Models.Paises
{
    public partial class Pais
    {
        [JsonProperty("id")]
        public PaiId? Id { get; set; }

        [JsonProperty("nome")]
        public Nome? Nome { get; set; }

        [JsonProperty("area")]
        public Area? Area { get; set; }

        [JsonProperty("localizacao")]
        public Localizacao? Localizacao { get; set; }

        [JsonProperty("linguas")]
        public List<Lingua>? Linguas { get; set; }

        [JsonProperty("governo")]
        public Governo? Governo { get; set; }

        [JsonProperty("unidades-monetarias")]
        public List<UnidadesMonetaria>? UnidadesMonetarias { get; set; }

        [JsonProperty("historico")]
        public string? Historico { get; set; }
    }

    public partial class Area
    {
        [JsonProperty("total")]
        public string? Total { get; set; }

        [JsonProperty("unidade")]
        public Unidade? Unidade { get; set; }
    }

    public partial class Unidade
    {
        [JsonProperty("nome")]
        public string? Nome { get; set; }

        [JsonProperty("símbolo")]
        public string? Símbolo { get; set; }

        [JsonProperty("multiplicador")]
        public long Multiplicador { get; set; }
    }

    public partial class Governo
    {
        [JsonProperty("capital")]
        public Capital? Capital { get; set; }
    }

    public partial class Capital
    {
        [JsonProperty("nome")]
        public string? Nome { get; set; }
    }

    public partial class PaiId
    {
        [JsonProperty("M49")]
        public long M49 { get; set; }

        [JsonProperty("ISO-3166-1-ALPHA-2")]
        public string? Iso31661Alpha2 { get; set; }

        [JsonProperty("ISO-3166-1-ALPHA-3")]
        public string? Iso31661Alpha3 { get; set; }
    }

    public partial class Lingua
    {
        [JsonProperty("id")]
        public LinguaId? Id { get; set; }

        [JsonProperty("nome")]
        public string? Nome { get; set; }
    }

    public partial class LinguaId
    {
        [JsonProperty("ISO-639-1")]
        public string? Iso6391 { get; set; }

        [JsonProperty("ISO-639-2")]
        public string? Iso6392 { get; set; }
    }

    public partial class Localizacao
    {
        [JsonProperty("regiao")]
        public Regiao? Regiao { get; set; }

        [JsonProperty("sub-regiao")]
        public Regiao? SubRegiao { get; set; }

        [JsonProperty("regiao-intermediaria")]
        public Regiao? RegiaoIntermediaria { get; set; }
    }

    public partial class Regiao
    {
        [JsonProperty("id")]
        public RegiaoId? Id { get; set; }

        [JsonProperty("nome")]
        public string? Nome { get; set; }
    }

    public partial class RegiaoId
    {
        [JsonProperty("M49")]
        public long M49 { get; set; }
    }

    public partial class Nome
    {
        [JsonProperty("abreviado")]
        public string? Abreviado { get; set; }

        [JsonProperty("abreviado-EN")]
        public string? AbreviadoEn { get; set; }

        [JsonProperty("abreviado-ES")]
        public string? AbreviadoEs { get; set; }
    }

    public partial class UnidadesMonetaria
    {
        [JsonProperty("id")]
        public UnidadesMonetariaId? Id { get; set; }

        [JsonProperty("nome")]
        public string? Nome { get; set; }
    }

    public partial class UnidadesMonetariaId
    {
        [JsonProperty("ISO-4217-ALPHA")]
        public string? Iso4217Alpha { get; set; }

        [JsonProperty("ISO-4217-NUMERICO")]
        public long? Iso4217Numerico { get; set; }
    }
}
