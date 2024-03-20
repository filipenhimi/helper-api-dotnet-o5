using helper_api_dotnet_o5.Controllers;
using helper_api_dotnet_o5.Models.G12Nba;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace helperapi_test_dotnet_o5.ControllersTest
{
  public class G12NbaControllerTest
  {
    [Fact]
    public void NbaGetTeam_West_DenverNugets_RetornoValido()
    {
      // Arrange
      var loggerMock = new Mock<ILogger<NbaController>>();
      var controller = new NbaController(loggerMock.Object);
      const Conference conference = Conference.West;

      // Act
      var response = controller.Get(conference, "Denver");
      ObjectResult objectResponse = Assert.IsType<OkObjectResult>(response);
      var model = Assert.IsAssignableFrom<List<Team>>(objectResponse.Value);

      // Assert
      Assert.IsType<OkObjectResult>(response);
      Assert.Equal(2, model.Count + 1);
      Assert.Equal("Denver Nuggets", model[0].FullName);
    }

    [Theory]
    [InlineData(Conference.None)]
    [InlineData(Conference.Invalid)]
    public void NbaGetTeam_InvalidConference_NoContent(Conference conference)
    {
      // Arrange
      var loggerMock = new Mock<ILogger<NbaController>>();
      var controller = new NbaController(loggerMock.Object);

      // Act
      var response = controller.Get(conference, "");

      // Assert
      Assert.IsType<NoContentResult>(response);
    }

  }
}
