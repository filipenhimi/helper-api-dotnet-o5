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

    /// <summary>
    /// Consultar Simulação de Investimento 
    /// </summary>
    /// <param name="valorInvestido">Valor Investido</param>
    /// <param name="period">Período Investito: 1 - Dias, 2 - Meses, 3 - Anos </param>
    /// <param name="periodAmount">Quantidade de (Dias,Meses,Anos)</param>
    /// <response code="200">Consultar simulação de investimento</response>
    /// <response code="404">Taxa do dia não encontrado</response>
    /// <response code="500">Oops! Não é possível realizar a simulação no momento</response>
    [Produces("application/json")]
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





