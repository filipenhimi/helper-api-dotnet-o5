using helper_api_dotnet_o5.Models.G9Pokedex;
using helper_api_dotnet_o5.Services;
using Microsoft.AspNetCore.Mvc;

namespace helper_api_dotnet_o5.Controllers;

[Route("api/[controller]")]
[ApiController]
public class G9PokedexController : ControllerBase
{
    private readonly IPokedexService _pokedexService;

    public G9PokedexController(IPokedexService pokedexService)
    {
        _pokedexService = pokedexService;
    }

    [HttpGet("pokemons/")]
    [ProducesResponseType(typeof(G9PokemonResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status502BadGateway)]
    public async Task<IActionResult> GetAllPokemon()
    {
        try
        {
            var pokemons = await _pokedexService.GetAllPokemonAsync();

            if (pokemons.Pokemon.Count > 0)
                return Ok(pokemons);
            else
                return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while fetching Pokémons.");
        }
    }

    [HttpGet("pokemons/{idPokemon}")]
    [ProducesResponseType(typeof(G9PokemonDataResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status502BadGateway)]
    public async Task<IActionResult> GetDataPokemon(int idPokemon)
    {
        try
        {
            var pokemonData = await _pokedexService.GetDataPokemonAsync(idPokemon);

            if (!string.IsNullOrEmpty(pokemonData.Name))
                return Ok(pokemonData);
            else
                return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while fetching Pokémon data.");
        }
       
    }
}
