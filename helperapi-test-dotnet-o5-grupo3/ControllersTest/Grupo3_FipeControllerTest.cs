using Microsoft.Extensions.Logging;
using helper_api_dotnet_o5.Controllers;
using Moq;
using helper_api_dotnet_o5.Fipe;
using Br.Com.Parallelum.Fipe.Model;
using Microsoft.AspNetCore.Mvc;

namespace helperapi_test_dotnet_o5_grupo3.ControllersTest;

public class Grupo3_FipeControllerTest
{
    [Theory]
    [InlineData(VehiclesType.Cars, "2010", "Celta Life")]
    [InlineData(VehiclesType.Motorcycles, "2010", "CG 125 FAN KS/ES/ESD")]
    [InlineData(VehiclesType.Trucks, "2010", "710 Plus")]

    public void FipeControllerRotaGet_QuandoApiRetornarAlgumResultado_EntaoHttpStatusOk200(VehiclesType type, string ano, string modelo)
    {
        //Arrange 
        var fipeServiceMock = new Mock<IFipeService>();
        var controller = GetController(fipeServiceMock);

        fipeServiceMock.Setup(x => x.GetFipeResult(It.IsAny<VehiclesType>(), It.IsAny<string>(), It.IsAny<string>()))
            .Returns(new List<InfoFipe> { new InfoFipe() }).Verifiable();

        //Act
        var result = controller.Get(type, ano, modelo);

        // Assert
        Assert.IsType<OkObjectResult>(result.Result);
    }

    private static FipeController GetController(Mock<IFipeService> fipeServiceMock)
    {
        var loggerMock = new Mock<ILogger<FipeController>>();
        return new FipeController(loggerMock.Object, fipeServiceMock.Object);
    }
}
