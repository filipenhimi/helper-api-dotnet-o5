using helper_api_dotnet_o5.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace helperapi_test_dotnet_o5.ControllersTest
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoedasControllerTest
    {
        [Fact]
        public void TestMethod()
        {

            var controller = new MoedasController();
            // Arrange
            var loggerMock = new Mock<ILogger<MoedasController>>();
            var codes = "USD-BRL,EUR-BRL";

            // Act
            var result = controller.GetLast(codes);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetLast_WhenCalledWithInvalidCodes_ReturnsNoContentResult()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<MoedasController>>();
            var controller = new MoedasController();
            var codes = "INVALID-CODE";

            // Act
            var result = await controller.GetLast(codes);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Theory]
        [InlineData("USD-BRL")]
        [InlineData("INVALID-CODE")]
        public async Task Get_WhenCalledWithValidOrInvalidCode_ReturnsCorrectResult(string code)
        {
            // Arrange
            var loggerMock = new Mock<ILogger<MoedasController>>();
            var controller = new MoedasController();
            var days = 1;

            // Act
            var result = await controller.Get(code, days);

            // Assert
            if (code == "USD-BRL")
                Assert.IsType<OkObjectResult>(result);
            else
                Assert.IsType<NoContentResult>(result);
        }

        [Theory]
        [InlineData("USD-BRL", 1)]
        [InlineData("INVALID-CODE", 1)]
        public async Task GetXml_WhenCalledWithValidOrInvalidCodeAndDays_ReturnsCorrectResult(string code, int days)
        {
            // Arrange
            var loggerMock = new Mock<ILogger<MoedasController>>();
            var controller = new MoedasController();

            // Act
            var result = await controller.GetXml(code, days);

            // Assert
            if (code == "USD-BRL")
                Assert.IsType<OkObjectResult>(result);
            else
                Assert.IsType<NoContentResult>(result);
        }
    }
}
