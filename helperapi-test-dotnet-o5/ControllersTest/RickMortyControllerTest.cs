
using helper_api_dotnet_o5.Controllers;
using helper_api_dotnet_o5.Infrastructure;
using helper_api_dotnet_o5.Models.Personagens;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace helperapi_test_dotnet_o5.ControllersTest
{
    public class RickMortyControllerTest
    {
        [Fact]
        public void ExecutaRotaGETPorID_QuandoAPIDisponivelEParametrosOK_EntaoRetornaObjetoValido()
        {
            //Arrange
            var loggerMock = new Mock<ILogger<RickMortyController>>();
            var controller = new RickMortyController(loggerMock.Object);

            //Act
            var result = controller.Get(1);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void ExecutaRotaGETComIdInvalido()
        {
            //Arrange
            var loggerMock = new Mock<ILogger<RickMortyController>>();
            var controller = new RickMortyController(loggerMock.Object);

            //Act
            var result = controller.Get(-1);

            //Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void ExecutaRotaGETBuscandoPorNomeComSucesso()
        {
            //Arrange
            var loggerMock = new Mock<ILogger<RickMortyController>>();
            var controller = new RickMortyController(loggerMock.Object);

            //Act
            var result = controller.SearchByName("rick");

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void ExecutaRotaGETBuscandoPorNomeInvalido()
        {
            //Arrange
            var loggerMock = new Mock<ILogger<RickMortyController>>();
            var controller = new RickMortyController(loggerMock.Object);

            //Act
            var result = controller.SearchByName("carlo");

            //Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
