using helper_api_dotnet_o5.Infrastructure;
using helper_api_dotnet_o5.Models.G9Pokedex;
using System.Globalization;

namespace helper_api_dotnet_o5.Services;

public class PokedexService : IPokedexService
{
    private const string ENDPOINT = "https://pokeapi.co/api/v2";
    private const string URLIMAGE = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/official-artwork";

    private readonly ILogger<PokedexService> _logger;
    public PokedexService(ILogger<PokedexService> logger)
    {
        _logger = logger;
    }
    public async Task<G9PokemonResponseDto> GetAllPokemonAsync()
    {
        var route = $"pokemon?limit=1306";
        var api = new HelperAPI(ENDPOINT);

        _logger.LogInformation("Buscando lista de pokemons.");

        var result = await api.MetodoGET<G9PokemonResultDto>(route);

        _logger.LogInformation($"Busca retornou {result.Results.Count} pokemons.");

        G9PokemonResponseDto pokemons = new G9PokemonResponseDto();

        if (result != null && result.Results.Count > 0)
        {
            foreach (var pokemon in result.Results)
            {
                string[] partes = pokemon.Url.Split('/');
                string ultimoElemento = partes[partes.Length - 2];

                string nomeFormatado = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(pokemon.Name.Replace('-', ' '));

                int idPokemon = Convert.ToInt32(ultimoElemento);

                pokemons.Pokemon.Add(new G9PokemonResponseDetailDto
                {
                    Name = nomeFormatado,
                    Id = idPokemon,
                    UrlImage = $"{URLIMAGE}/{idPokemon}.png"
            });
            }
        }

        return pokemons;
    }

    public async Task<G9PokemonDataResponseDto> GetDataPokemonAsync(int idPokemon)
    {
        var routeSpecie = $"pokemon-species/{idPokemon}/";
        var routePokemon = $"pokemon/{idPokemon}/";

        var api = new HelperAPI(ENDPOINT);

        _logger.LogInformation($"Buscando informações da espécie do pokemon ID {idPokemon}.");

        var resultSpecie = await api.MetodoGET<G9PokemonSpecieResultDto>(routeSpecie);

      
        G9PokemonDataResponseDto pokemonData = new G9PokemonDataResponseDto();

        _logger.LogInformation($"Buscando informações do pokemon ID {idPokemon}.");

        var pokemonResult = await api.MetodoGET<G9PokemonDataResultDto>(routePokemon);

        _logger.LogInformation($"Retornou a busca do pokemon {GetNameModify(pokemonResult.Name)}.");

        if (pokemonResult != null)
        {
            pokemonData.Weight = pokemonResult.Weight;
            pokemonData.Height = pokemonResult.Height;
            pokemonData.Name = GetNameModify(pokemonResult.Name);
            pokemonData.UrlImage = $"{URLIMAGE}/{pokemonResult.Id}.png";

            foreach (var stat in pokemonResult.Stats)
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

            var resultEvolution = await api.MetodoGET<G9PokemonEvolutionResultDto>(parteDepoisDeV2);

            if (resultEvolution != null && resultEvolution.Chain != null)
            {
                var evolutions = GetAllEvolvesTo(resultEvolution.Chain);

                foreach (var evolution in evolutions)
                {
                    string[] partes = evolution.Url.Split('/');
                    string ultimoElemento = partes[partes.Length - 2];

                    pokemonData.Evolutions.Add(new G9PokemonDataEvolutionDto
                    {
                        Name = GetNameModify(evolution.Name),
                        UrlImage = $"{URLIMAGE}/{ultimoElemento}.png"
                    });
                }
            }
        }

        return pokemonData;
    }

    private string GetNameModify(string name)
    {
        return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(name.Replace('-', ' '));
    }

    private List<Species> GetAllEvolvesTo(Chain chain)
    {
        List<Species> todasEvolucoes = new List<Species>
        {
            // Adiciona a espécie atual
            chain.Species
        };

        // Para cada Chain dentro de EvolvesTo, obtém todas as EvolvesTo recursivamente
        foreach (var evolveTo in chain.EvolvesTo)
        {
            todasEvolucoes.AddRange(GetAllEvolvesTo(evolveTo));
        }

        return todasEvolucoes;
    }
}
