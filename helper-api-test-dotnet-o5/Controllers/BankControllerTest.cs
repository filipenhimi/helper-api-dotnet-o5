using helper_api_dotnet_o5.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace helper_api_test_dotnet_o5.Controllers
{
    public class BankControllerTest
    {
        [Fact]
        public void Deve_retornar_ok_object_result_quando_api_estah_disponivel()
        {
            //Arrange
            var loggerMock = new Mock<ILogger<BankController>>();
            var sut = new BankController(loggerMock.Object);

            //Act
            var result = sut.Get("banco");

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
