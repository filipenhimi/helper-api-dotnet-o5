using helper_api_dotnet_o5.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace helperapi_test_dotnet_o5.ControllersTest
{
    public class CoincapControllerTest
    {
        [Fact]
        public void ExecuteRouteAssetsGET_Should_be_able_return_a_valid_data_object()
        {
            var loggerMock = new Mock<ILogger<CoincapController>>();
            var controller = new CoincapController(loggerMock.Object);

            var result = controller.Index();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void ExecuteRouteAssetsDetailGET_Should_be_able_return_a_valid_data_object()
        {
            var loggerMock = new Mock<ILogger<CoincapController>>();
            var controller = new CoincapController(loggerMock.Object);

            var result = controller.Detail("bitcoin");

            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public void ExecuteRouteAssetsDetailGET_should_be_able_return_no_content_when_the_passed_parameter_is_invalid()
        {
            var loggerMock = new Mock<ILogger<CoincapController>>();
            var controller = new CoincapController(loggerMock.Object);

            var result = controller.Detail("bbb");

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void ExecuteRouteAsssetsHistoryGET_should_be_able_return_a_valid_data_object()
        {
            var loggerMock = new Mock<ILogger<CoincapController>>();
            var controller = new CoincapController(loggerMock.Object);

            var result = controller.History("bitcoin", "d1");

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void ExecuteRouteAsssetsHistoryGET_should_be_able_return_no_content_when_the_passed_parameter_is_invalid()
        {
            var loggerMock = new Mock<ILogger<CoincapController>>();
            var controller = new CoincapController(loggerMock.Object);

            var result = controller.History("bitcoi", "1");

            Assert.IsType<NoContentResult>(result);
        }
    }
}
