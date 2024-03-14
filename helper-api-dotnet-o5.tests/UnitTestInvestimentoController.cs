namespace helper_api_dotnet_o5.tests;

using helper_api_dotnet_o5.Controllers;
using helper_api_dotnet_o5.Services;
using helper_api_dotnet_o5.Util.DTOs;
using helper_api_dotnet_o5.Util.Enums;
using helper_api_dotnet_o5.Util.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit.Abstractions;

public class UnitTestInvestimentoController
{
    private readonly ITestOutputHelper output;

    public UnitTestInvestimentoController(ITestOutputHelper output)
    {
        this.output = output;
    }


    [Fact]
    public async Task Get_DeveRetornarOkSimulacaoInvestimento()
    {
        var inMemorySettings = new Dictionary<string, string> {
            {"URL_API_GOV", "https://api.bcb.gov.br"}
        };

        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        var loggerMock = new Mock<ILogger<InvestimentoController>>();
        var simulacaoInvestimentoServiceMock = new Mock<SimulacaoInvestimentoService>(configuration);
        
        if (loggerMock.Object is null)
        {
            throw new NullReferenceException(nameof(loggerMock.Object));
        }

        if(simulacaoInvestimentoServiceMock.Object is null)
        {
            throw new NullReferenceException(nameof(simulacaoInvestimentoServiceMock.Object));
        }


        var controller = new InvestimentoController(loggerMock.Object, simulacaoInvestimentoServiceMock.Object);

        var result = await controller.SimulacaoInvestimentoCDI(2000.00M, (EPeriod)2, 6);

        Assert.IsType<OkObjectResult>(result.Result);
    }

}