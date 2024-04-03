using helper_api_dotnet_o5.Controllers;
using helper_api_dotnet_o5.Models.G9Pokedex;
using helper_api_dotnet_o5.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;

namespace helperapi_test_dotnet_o5_grupo9.ControllersTest;

public class PokedexControllerTest
{
    [Fact]
    public async Task GetAllPokemon_ReturnsOkWithPokemonList()
    {
        // Arrange
        var mockService = new Mock<IPokedexService>();

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
        };

        var expectedResponse = new G9PokemonResponseDto { Pokemon = pokemonsData };

        mockService.Setup(x => x.GetAllPokemonAsync())
                   .ReturnsAsync(expectedResponse);

        var controller = new G9PokedexController(mockService.Object);

        // Act
        var result = await controller.GetAllPokemon() as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        Assert.NotNull(result.Value);

        var pokemons = Assert.IsType<G9PokemonResponseDto>(result.Value);

        Assert.Equal(2, pokemons.Pokemon.Count);
        Assert.Contains("Pikachu", pokemons.Pokemon.Select(p => p.Name)); // Verifica se Pikachu está presente na lista de nomes
        Assert.Contains("Charizard", pokemons.Pokemon.Select(p => p.Name)); // Verifica se Charizard está presente na lista de nomes
    }

    [Fact]
    public async Task GetAllPokemon_ReturnsNoContentWhenEmpty()
    {
        // Arrange
        var mockService = new Mock<IPokedexService>();
        var expectedResponse = new G9PokemonResponseDto { Pokemon = new List<G9PokemonResponseDetailDto>() };

        mockService.Setup(x => x.GetAllPokemonAsync())
                   .ReturnsAsync(expectedResponse);

        var controller = new G9PokedexController(mockService.Object);

        // Act
        var result = await controller.GetAllPokemon() as NoContentResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal((int)HttpStatusCode.NoContent, result.StatusCode);
    }

    [Fact]
    public async Task GetDataPokemon_ReturnsOkWithPokemonData()
    {
        // Arrange
        var mockService = new Mock<IPokedexService>();
        var expectedResponse = new G9PokemonDataResponseDto { Name = "Pikachu", Types = new List<string> { "Electric" } };
        var idPokemon = 25;

        mockService.Setup(x => x.GetDataPokemonAsync(idPokemon))
                   .ReturnsAsync(expectedResponse);

        var controller = new G9PokedexController(mockService.Object);

        // Act
        var result = await controller.GetDataPokemon(idPokemon) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        Assert.NotNull(result.Value);

        var pokemonData = Assert.IsType<G9PokemonDataResponseDto>(result.Value);
        Assert.Equal("Pikachu", pokemonData.Name);
        Assert.Contains("Electric", pokemonData.Types);
    }

    [Fact]
    public async Task GetDataPokemon_ReturnsNoContentWhenNotFound()
    {
        // Arrange
        var mockService = new Mock<IPokedexService>();
        var expectedResponse = new G9PokemonDataResponseDto();
        var idPokemon = 9999;

        mockService.Setup(x => x.GetDataPokemonAsync(idPokemon))
                   .ReturnsAsync(expectedResponse);

        var controller = new G9PokedexController(mockService.Object);

        // Act
        var result = await controller.GetDataPokemon(idPokemon) as NoContentResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal((int)HttpStatusCode.NoContent, result.StatusCode);
    }

    [Fact]
    public async Task GetAllPokemon_ReturnsInternalServerErrorWhenServiceFails()
    {
        // Arrange
        var mockService = new Mock<IPokedexService>();

        mockService.Setup(x => x.GetAllPokemonAsync())
                   .ThrowsAsync(new Exception("Simulated exception"));

        var controller = new G9PokedexController(mockService.Object);

        // Act
        var result = await controller.GetAllPokemon() as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal((int)HttpStatusCode.InternalServerError, result.StatusCode);
    }
}
