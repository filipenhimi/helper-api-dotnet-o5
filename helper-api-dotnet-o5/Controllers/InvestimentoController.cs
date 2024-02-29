using helper_api_dotnet_o5.Util.DTOs;
using helper_api_dotnet_o5.Util.Enums;
using helper_api_dotnet_o5.Util.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using RestSharp.Authenticators;
using System.Text.Json;
using System.Threading;

namespace helper_api_dotnet_o5.Controllers;
[Route("api/[controller]")]
[ApiController]
public class InvestimentoController : ControllerBase
{
    private ILogger<InvestimentoController> _logger { get; }
    private readonly ISimulacaoInvestimentoService _simulacaoInvestimentoService;
    public InvestimentoController(ILogger<InvestimentoController> logger, ISimulacaoInvestimentoService simulacaoInvestimentoService)
    {
        _logger = logger;
        _simulacaoInvestimentoService = simulacaoInvestimentoService;
    }

    [HttpGet]
    public async Task<ActionResult<SimulacaoInvestimentoDTO>> SimulacaoInvestimentoCDI([FromQuery(Name = "valorInvestido")] Decimal valorInvestido, [FromQuery(Name = "period")] EPeriod period, [FromQuery(Name = "periodAmount")] int periodAmount)
    {
        try
        {
            var simulacaoInvestimento = await _simulacaoInvestimentoService.SimulacaoInvestimento(valorInvestido, period, periodAmount);

            return Ok(simulacaoInvestimento);
        }
        catch (Exception ex)
        {
            var error = BadRequest(new { errorMessage = ex.Message });

            return error;
        }
    }
}





