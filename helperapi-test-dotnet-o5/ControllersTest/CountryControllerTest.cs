using helper_api_dotnet_o5.Controllers;
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
    }
}
