using helper_api_dotnet_o5.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace helperapi_test_dotnet.ControllersTest
{
    public class ExchangeRateControllerTest
    {
        [Fact]
        public void ExecutaRotaCotacaoGET_QuandoAPIDisponivelEParametroOK_EntaoRetornaObjetoValido()
        {
            //Arrange
            var loggerMock = new Mock<ILogger<ExchangeRateController>>();
            var controller = new ExchangeRateController(loggerMock.Object);

            //Act
            var result = controller.Get("USD-BRL", 1);

            //Assert
            Assert.IsType<OkObjectResult>(result);

        }

        [Fact]
        public void ExecutaRotaCotacaoGET_QuandoMoedaInvalidaEntaoRetornaNotFound()
        {
            //Arrange
            var loggerMock = new Mock<ILogger<ExchangeRateController>>();
            var controller = new ExchangeRateController(loggerMock.Object);

            //Act
            var result = controller.Get("USD-BXX", 0);

            //Assert
            Assert.IsType<StatusCodeResult>(result);
        }
    }
}