namespace helper_api_dotnet_o5.Models.G9Pokedex;

public class G9PokemonDataResponseDto
{
    public string UrlImage { get; set; }
    public string Name { get; set; }
    public IList<string> Types { get; set; } = new List<string>();
    public long Height { get; set; }
    public long Weight { get; set; }
    public IList<string> Abilities { get; set; } = new List<string>();
    public IList<string> Moves { get; set; } = new List<string>();
    public IList<G9PokemonDataStatDto> Stats { get; set; } = new List<G9PokemonDataStatDto>();
    public IList<G9PokemonDataEvolutionDto> Evolutions { get; set; } = new List<G9PokemonDataEvolutionDto>();
}

public class G9PokemonDataEvolutionDto
{
    public string Name { get; set; }
    public string UrlImage { get; set; }
}

public class G9PokemonDataStatDto
{
    public string Name { get; set; }
    public long BaseStat { get; set; }
}
