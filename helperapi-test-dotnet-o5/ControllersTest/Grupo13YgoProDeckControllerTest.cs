using helper_api_dotnet_o5.Controllers;
using helper_api_dotnet_o5.Models.Grupo13;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Diagnostics;

namespace helperapi_test_dotnet_o5.ControllersTest
{
    public class Grupo13YgoProDeckControllerTest
    {
        [Fact]
        public void ExecutaRotaGrupo13YgoProDeckGET_QuandoAPIDisponivelEParametrosOK_EntaoRetornaObjetoValido()
        {
            //Arrange
            var loggerMock = new Mock<ILogger<Grupo13YgoProDeckController>>();
            var controller = new Grupo13YgoProDeckController(loggerMock.Object);
            CardRequestDto request = new();
            request.fname = "Blue-Eyes White Dragon";

            //Act
            var result = controller.Get(request);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void ExecutaRotaGrupo13YgoProDeckGET_QuandoAPIDisponivelEParametrosInvalidos_EntaoRetornaNoContent()
        {
            //Arrange
            var loggerMock = new Mock<ILogger<Grupo13YgoProDeckController>>();
            var controller = new Grupo13YgoProDeckController(loggerMock.Object);
            CardRequestDto request = new();
            request.fname = "Testando";

            //Act
            var result = controller.Get(request);

            //Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void ExecutaRotaGrupo13YgoProDeckGET_QuandoAPIDisponivelEParametroFNameNulo_EntaoRetornaOK()
        {
            //Arrange
            var loggerMock = new Mock<ILogger<Grupo13YgoProDeckController>>();
            var controller = new Grupo13YgoProDeckController(loggerMock.Object);
            CardRequestDto request = new();
            request.fname = "";

            //Act
            var result = controller.Get(request);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void ExecutaRotaGrupo13YgoProDeckGET_QuandoAPIDisponivelEParametroRacaNulo_EntaoRetornaOK()
        {
            //Arrange
            var loggerMock = new Mock<ILogger<Grupo13YgoProDeckController>>();
            var controller = new Grupo13YgoProDeckController(loggerMock.Object);
            CardRequestDto request = new();
            request.race = null;

            //Act
            var result = controller.Get(request);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Theory]
        [InlineData("Aqua")]
        [InlineData("Beast")]
        [InlineData("BeastWarrior")]
        [InlineData("CreatorGod")]
        [InlineData("Cyberse")]
        [InlineData("Dinosaur")]
        [InlineData("DivineBeast")]
        [InlineData("Dragon")]
        [InlineData("Fairy")]
        [InlineData("Fiend")]
        [InlineData("Fish")]
        [InlineData("Insect")]
        [InlineData("Machine")]
        [InlineData("Plant")]
        [InlineData("Psychic")]
        [InlineData("Pyro")]
        [InlineData("Reptile")]
        [InlineData("Rock")]
        [InlineData("SeaSerpent")]
        [InlineData("Spellcaster")]
        [InlineData("Thunder")]
        [InlineData("Warrior")]
        [InlineData("WingedBeast")]
        [InlineData("Wyrm")]
        [InlineData("Zombie")]
        public void ExecutaRotaPaisesGET_QuandoAPIDisponivelEParametrosOK_EntaoRetornaObjetoValido(string race)
        {
            //Arrange
            var loggerMock = new Mock<ILogger<Grupo13YgoProDeckController>>();
            var controller = new Grupo13YgoProDeckController(loggerMock.Object);
            CardRequestDto request = new();
            request.race = (RaceEnum)Enum.Parse(typeof(RaceEnum), race);

            //Act
            var result = controller.Get(request);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void ExecutaRotaGrupo13YgoProDeckGETRandom_QuandoAPIDisponivelEParametrosOK_EntaoRetornaObjetoValido()
        {
            //Arrange
            var loggerMock = new Mock<ILogger<Grupo13YgoProDeckController>>();
            var controller = new Grupo13YgoProDeckController(loggerMock.Object);

            //Act
            var result = controller.GetRandom();

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
