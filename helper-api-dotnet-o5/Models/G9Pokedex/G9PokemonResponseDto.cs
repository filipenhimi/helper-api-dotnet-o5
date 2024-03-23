namespace helper_api_dotnet_o5.Models.G9Pokedex;

public class G9PokemonResponseDto
{
    public IList<G9PokemonResponseDetailDto> Pokemon { get; set; } = new List<G9PokemonResponseDetailDto>();
}

public class G9PokemonResponseDetailDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string UrlImage { get; set; }
}
