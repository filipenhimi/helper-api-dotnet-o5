using helper_api_dotnet_o5.Models.G9Pokedex;
using helper_api_dotnet_o5.Services;
using Moq;

namespace helperapi_test_dotnet_o5_grupo9.InfrastructureTest;

public class PokedexServiceTest
{
    [Fact]
    public async Task GetAllPokemonAsync_ReturnsPokemonList()
    {
        // Arrange
        var pokemonsData = new List<G9PokemonResponseDetailDto>
        {
            new G9PokemonResponseDetailDto
            {
                Name = "Pikachu"
            },

            new G9PokemonResponseDetailDto
            {
                 Name = "Charizard"
            },

            new G9PokemonResponseDetailDto
            {
                 Name = "Squirtle"
            }
        };
        var mockResponseDto = new G9PokemonResponseDto { Pokemon = pokemonsData };
        var mockPokedexService = new Mock<IPokedexService>();
        mockPokedexService.Setup(service => service.GetAllPokemonAsync()).ReturnsAsync(mockResponseDto);
        var pokedexService = mockPokedexService.Object;

        // Act
        var result = await pokedexService.GetAllPokemonAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(3, result.Pokemon.Count); // Verificando se três Pokémon foram retornados
    }

    [Fact]
    public async Task GetDataPokemonAsync_ReturnsPokemonData()
    {
        // Arrange
        var expectedPokemonData = new G9PokemonDataResponseDto { Name = "Pikachu", Types = new List<string> { "Electric" } };
        var mockPokedexService = new Mock<IPokedexService>();
        mockPokedexService.Setup(service => service.GetDataPokemonAsync(It.IsAny<int>())).ReturnsAsync(expectedPokemonData);
        var pokedexService = mockPokedexService.Object;

        // Act
        var result = await pokedexService.GetDataPokemonAsync(25); // ID do Pikachu é 25

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Pikachu", result.Name); // Verifica se o nome do Pokémon retornado é Pikachu
        Assert.Contains("Electric", result.Types); // Verifica se o tipo do Pokémon retornado é Electric
    }

    [Fact]
    public async Task GetDataPokemonAsync_ThrowsException_WhenIdIsInvalid()
    {
        // Arrange
        var mockPokedexService = new Mock<IPokedexService>();
        mockPokedexService.Setup(service => service.GetDataPokemonAsync(It.IsAny<int>())).ThrowsAsync(new ArgumentException());
        var pokedexService = mockPokedexService.Object;

        // Act e Assert
        await Assert.ThrowsAsync<ArgumentException>(() => pokedexService.GetDataPokemonAsync(-1)); // ID inválido
    }
}
