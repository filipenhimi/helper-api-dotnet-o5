using helper_api_dotnet_o5.Controllers;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace helperapi_test_dotnet_o5.ControllersTest
{
    public class CountryControllerTest
    {
        [Fact]
        public void ExecutaRotaPaisesGET_QuandoAPIDisponivelEParametrosOK_EntaoRetornaObjetoValido()
        {
            //Arrange
            var loggerMock = new Mock<ILogger<CountryController>>();
            var controller = new CountryController(loggerMock.Object);

            //Act
            var result = controller.Get("BR");

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void ExecutaRotaPaisesGET_QuandoAPIDisponivelEParametrosInvalidos_EntaoRetornaNoContent()
        {
            //Arrange
            var loggerMock = new Mock<ILogger<CountryController>>();
            var controller = new CountryController(loggerMock.Object);

            //Act
            var result = controller.Get("teste");

            //Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Theory]
        [InlineData("BR")]
        [InlineData("TESTE")]
        public void ExecutaRotaPaisesGET_QuandoAPIDisponivelEParametrosOK_EntaoRetornaObjetoValidoOuInvalido(string sigla)
        {
            //Arrange
            var loggerMock = new Mock<ILogger<CountryController>>();
            var controller = new CountryController(loggerMock.Object);

            //Act
            var result = controller.Get(sigla);

            //Assert
            if (sigla == "BR")
                Assert.IsType<OkObjectResult>(result);
            else
                Assert.IsType<NoContentResult>(result);
        }
    }
}
