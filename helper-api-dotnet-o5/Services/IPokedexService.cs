using helper_api_dotnet_o5.Models.G9Pokedex;

namespace helper_api_dotnet_o5.Services;

public interface IPokedexService
{
    Task<G9PokemonResponseDto> GetAllPokemonAsync();
    Task<G9PokemonDataResponseDto> GetDataPokemonAsync(int idPokemon);
}
