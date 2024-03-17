using Br.Com.Parallelum.Fipe.Api;
using Br.Com.Parallelum.Fipe.Model; 
using helper_api_dotnet_o5.Fipe;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Moq;

namespace helperapi_test_dotnet_o5_grupo3.InfrastructureTest;

public class FipeServiceTest
{
    [Theory]
    [InlineData(VehiclesType.Cars, "2001", "Modelo 1")]
    [InlineData(VehiclesType.Cars, "2002", "Modelo 2")]
    [InlineData(VehiclesType.Cars, "2003", "Modelo 3")]
    [InlineData(VehiclesType.Cars, "2004", "Modelo 4")]
    public void TestFipeResult(VehiclesType type, string ano, string modelo)
    {
        //Arrange
        var fipeService = GetServiceWithMocks();

        //Act
        var fipeResult = fipeService.GetFipeResult(type, ano, modelo);

        //Assert
        Assert.IsType<List<InfoFipe>>(fipeResult);
    }

    private static FipeService GetServiceWithMocks()
    {
        var fipeService = GetService();

        //// Instanciando usando NBuilder
        //var marcas = Builder<NamedCode>.CreateListOfSize(2)
        //    .TheFirst(1)
        //    .With(x => x.Name = "Marca 1")
        //    .With(x => x.Code = "1")
        //    .TheNext(1)
        //    .With(x => x.Name = "Marca 2")
        //    .With(x => x.Code = "2")
        //    .Build();

        // Instanciando Diretamente
        var marcas = new List<NamedCode> { new NamedCode("Marca 1", "1"), new NamedCode("Marca 2", "2") };

        var modelosMarca1 = new List<NamedCode> { new NamedCode("Modelo 1", "1"), new NamedCode("Modelo 2", "2") };
        var modelosMarca2 = new List<NamedCode> { new NamedCode("Modelo 3", "3"), new NamedCode("Modelo 4", "4") };

        var anosMod1 = new List<NamedCode> { new NamedCode("2001-1", "2001-1"), new NamedCode("2001-2", "2001-2") };
        var anosMod2 = new List<NamedCode> { new NamedCode("2002-1", "2002-1"), new NamedCode("2002-2", "2002-2") };
        var anosMod3 = new List<NamedCode> { new NamedCode("2003-1", "2003-1"), new NamedCode("2003-2", "2003-2") };
        var anosMod4 = new List<NamedCode> { new NamedCode("2004-1", "2004-1"), new NamedCode("2004-2", "2004-2") };

        Mock.Get(fipeService.ApiInstance)
            .Setup(x => x.GetBrandsByTypeAsync(It.IsAny<VehiclesType>(), new CancellationToken()))
            .ReturnsAsync(marcas.ToList());
        Mock.Get(fipeService.ApiInstance)
            .Setup(x => x.GetModelsByBrandAsync(It.IsAny<VehiclesType>(), 1, new CancellationToken()))
            .ReturnsAsync(modelosMarca1);
        Mock.Get(fipeService.ApiInstance)
            .Setup(x => x.GetModelsByBrandAsync(It.IsAny<VehiclesType>(), 2, new CancellationToken()))
            .ReturnsAsync(modelosMarca2);

        Mock.Get(fipeService.ApiInstance)
            .Setup(x => x.GetYearByModelAsync(It.IsAny<VehiclesType>(), 1, 1, new CancellationToken()))
            .ReturnsAsync(anosMod1);
        Mock.Get(fipeService.ApiInstance)
            .Setup(x => x.GetYearByModelAsync(It.IsAny<VehiclesType>(), 1, 2, new CancellationToken()))
            .ReturnsAsync(anosMod2);
        Mock.Get(fipeService.ApiInstance)
            .Setup(x => x.GetYearByModelAsync(It.IsAny<VehiclesType>(), 2, 3, new CancellationToken()))
            .ReturnsAsync(anosMod3);
        Mock.Get(fipeService.ApiInstance)
            .Setup(x => x.GetYearByModelAsync(It.IsAny<VehiclesType>(), 2, 4, new CancellationToken()))
            .ReturnsAsync(anosMod4);

        Mock.Get(fipeService.ApiInstance)
            .Setup(x => x.GetFipeInfo(It.IsAny<VehiclesType>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), 0))
            .Returns(new FipeResult());

        Mock.Get(fipeService.ApiInstance)
            .Setup(x => x.GetFipeInfoAsync(It.IsAny<VehiclesType>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), 0, new CancellationToken()))
            .ReturnsAsync(new FipeResult());

        return fipeService;
    }

    private static FipeService GetService()
    {
        var loggerMock = new Mock<ILogger<FipeService>>();
        var memoryCacheMock = new Mock<IMemoryCache>();
        var fipeApiMock = new Mock<IFipeApi>();

        memoryCacheMock.Setup(x => x.CreateEntry(It.IsAny<object>())).Returns(Mock.Of<ICacheEntry>());

        return new FipeService(loggerMock.Object, memoryCacheMock.Object, fipeApiMock.Object);
    }
}
