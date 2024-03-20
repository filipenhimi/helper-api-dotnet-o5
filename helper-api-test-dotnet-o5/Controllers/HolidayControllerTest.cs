using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using helper_api_dotnet_o5.Controllers;
using helper_api_dotnet_o5.Models.Feriados;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace helper_api_test_dotnet_o5.Controllers
{
    public class HolidayControllerTest
    {

        [Fact]
        public async Task Get_DeveRetornarOkComListaDeFeriados()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<HolidayController>>();

            var controller = new HolidayController(loggerMock.Object);

            // Act
            var result = controller.Get("2024");

            // Assert
            Assert.IsType<OkObjectResult>(result);
;
        }
        
    }
}