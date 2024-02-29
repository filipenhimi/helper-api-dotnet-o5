using helper_api_dotnet_o5.Infrastructure;
using helper_api_dotnet_o5.Models.G9Pokedex;
using helper_api_dotnet_o5.Models.Paises;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;

namespace helper_api_dotnet_o5.Controllers;

[Route("api/[controller]")]
[ApiController]
public class G9PokedexController : ControllerBase
{
    private const string ENDPOINT = "https://pokeapi.co/api/v2";
    private readonly ILogger<G9PokedexController> _logger;

    public G9PokedexController(ILogger<G9PokedexController> logger)
    {
        _logger = logger;
    }

    [HttpGet("pokemons/")]
    [ProducesResponseType(typeof(List<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status502BadGateway)]
    public async Task<IActionResult> GetAllPokemon()
    {
        var route = $"pokemon?limit=1306";
        var api = new HelperAPI(ENDPOINT);
        var result = await api.MetodoGET<G9PokemonResult>(route);

        G9PokemonResponseDto pokemons  = new G9PokemonResponseDto();

        if(result != null && result.Results.Count > 0)
        {
            foreach (var pokemon in result.Results)
            {
                string[] partes = pokemon.Url.Split('/');
                string ultimoElemento = partes[partes.Length - 2];

                string nomeFormatado = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(pokemon.Name.Replace('-', ' '));

                pokemons.Pokemon.Add(new G9PokemonResponseDetailDto
                {
                    Name = nomeFormatado,
                    Id = Convert.ToInt32(ultimoElemento)
                });
            }
        }
        
        if(pokemons.Pokemon.Count > 0)
            return Ok(pokemons);
        else
            return NoContent();
    }

    private string GetNameModify(string name)
    {
        return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(name.Replace('-', ' '));
    }

    private List<Species> GetAllEvolvesTo(Chain chain)
    {
        List<Species> todasEvolucoes = new List<Species>();

        // Adiciona a espécie atual
        todasEvolucoes.Add(chain.Species);

        // Para cada Chain dentro de EvolvesTo, obtém todas as EvolvesTo recursivamente
        foreach (var evolveTo in chain.EvolvesTo)
        {
            todasEvolucoes.AddRange(GetAllEvolvesTo(evolveTo));
        }

        return todasEvolucoes;
    }

    [HttpGet("pokemons/{idPokemon}")]
    [ProducesResponseType(typeof(List<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status502BadGateway)]
    public async Task<IActionResult> GetDataPokemon(int idPokemon)
    {
        var routeSpecie = $"pokemon-species/{idPokemon}/";
        var routePokemon = $"pokemon/{idPokemon}/";

        var api = new HelperAPI(ENDPOINT);
        var resultSpecie = await api.MetodoGET<G9PokemonSpecieResult>(routeSpecie);

        G9PokemonDataResponseDto pokemonData = new G9PokemonDataResponseDto();

        var pokemonResult = await api.MetodoGET<G9PokemonDataResult>(routePokemon);

        if(pokemonResult != null)
        {
            pokemonData.Weight = pokemonResult.Weight;
            pokemonData.Height = pokemonResult.Height;
            pokemonData.Name = GetNameModify(pokemonResult.Name);
            pokemonData.UrlImage = $"https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/official-artwork/{pokemonResult.Id}.png";

            foreach(var stat in pokemonResult.Stats)
            {
                pokemonData.Stats.Add(new G9PokemonDataStatDto
                {
                    Name = GetNameModify(stat.StatDetail.Name),
                    BaseStat = stat.BaseStat
                });
            }

            foreach (var move in pokemonResult.Moves)
            {
                pokemonData.Moves.Add(GetNameModify(move.MoveDetail.Name));
            }

            foreach (var ability in pokemonResult.Abilities)
            {
                pokemonData.Abilities.Add(GetNameModify(ability.AbilityDetail.Name));
            }

            foreach (var type in pokemonResult.Types)
            {
                pokemonData.Types.Add(GetNameModify(type.TypeDetail.Name));
            }
        }


        if (resultSpecie != null && !string.IsNullOrWhiteSpace(resultSpecie.EvolutionChain?.Url))
        {
            var url = resultSpecie.EvolutionChain.Url;

            // Encontra a posição de "v2" na URL
            int posicaoV2 = url.IndexOf("v2");

            // Obtém a parte da URL após "v2"
            string parteDepoisDeV2 = url.Substring(posicaoV2 + 2);

            var resultEvolution = await api.MetodoGET<G9PokemonEvolutionResult>(parteDepoisDeV2);

            if(resultEvolution != null && resultEvolution.Chain != null)
            {
                var evolutions = GetAllEvolvesTo(resultEvolution.Chain);

                foreach( var evolution in evolutions )
                {
                    string[] partes = evolution.Url.Split('/');
                    string ultimoElemento = partes[partes.Length - 2];

                    pokemonData.Evolutions.Add(new G9PokemonDataEvolutionDto
                    {
                        Name = GetNameModify(evolution.Name),
                        UrlImage = $"https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/official-artwork/{ultimoElemento}.png"
                    });
                }
            }
        }

       if (!string.IsNullOrEmpty(pokemonData.Name))
            return Ok(pokemonData);
       else
           return NoContent();
    }
}
