using Newtonsoft.Json;

namespace helper_api_dotnet_o5.Models.G9Pokedex;

public class G9PokemonDataResult
{
    [JsonProperty("abilities")]
    public List<Ability> Abilities { get; set; }

    [JsonProperty("height")]
    public long Height { get; set; }

    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("moves")]
    public List<Move> Moves { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("stats")]
    public List<Stat> Stats { get; set; }

    [JsonProperty("types")]
    public List<Type> Types { get; set; }

    [JsonProperty("weight")]
    public long Weight { get; set; }
}

public partial class AbilityDetail
{
    [JsonProperty("name")]
    public string Name { get; set; }
}

public partial class Ability
{
    [JsonProperty("ability")]
    public AbilityDetail AbilityDetail { get; set; }

    [JsonProperty("is_hidden")]
    public bool IsHidden { get; set; }

    [JsonProperty("slot")]
    public long Slot { get; set; }
}

public partial class MoveDetail
{
    [JsonProperty("name")]
    public string Name { get; set; }
}
public partial class Move
{
    [JsonProperty("move")]
    public MoveDetail MoveDetail { get; set; }
}

public partial class StatDetail
{
    [JsonProperty("name")]
    public string Name { get; set; }
}

public partial class Stat
{
    [JsonProperty("base_stat")]
    public long BaseStat { get; set; }

    [JsonProperty("stat")]
    public StatDetail StatDetail { get; set; }
}

public partial class TypeDetail
{
    [JsonProperty("name")]
    public string Name { get; set; }
}

public partial class Type
{
    [JsonProperty("slot")]
    public long Slot { get; set; }

    [JsonProperty("type")]
    public TypeDetail TypeDetail { get; set; }
}