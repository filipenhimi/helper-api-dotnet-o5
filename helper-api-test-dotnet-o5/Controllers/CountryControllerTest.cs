using helper_api_dotnet_o5.Controllers;
using helper_api_dotnet_o5.Models.Paises;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace helper_api_test_dotnet_o5.Controllers
{
    public class CountryControllerTest
    {
        [Fact]
        public async Task Get_DeveRetornarOkComListaDePaises()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<CountryController>>();

            var controller = new CountryController(loggerMock.Object);

            // Act
            var result = controller.Get("BR");

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

    }
}
