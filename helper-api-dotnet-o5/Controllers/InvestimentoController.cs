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
    public InvestimentoController(ILogger<InvestimentoController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<SimulacaoInvestimentoDTO>> SimulacaoInvestimentoCDI([FromQuery] Decimal valorInvestido, [FromQuery] EPeriod period, [FromQuery] int periodAmount)
    {
        try
        {
            var simulacaoInvestimentoService = new SimulacaoInvestimentoService();
            var simulacaoInvestimento = await simulacaoInvestimentoService.SimulacaoInvestimento(valorInvestido, period, periodAmount);

            return Ok(simulacaoInvestimento);
        }
        catch (Exception ex)
        {
            var error = BadRequest(new { errorMessage = ex.Message });

            return error;
        }
    }
}


public enum EPeriod
{
    Days = 1,
    Months = 2,
    Years = 3
}

public class Investimento
{
    public Decimal taxaAplicada { get; set; } = 0.0000M;
    public Decimal jurosPago { get; set; } = 0.00M;
}

public class SimulacaoInvestimentoDTO
{
    public Decimal Investido { get; set; } = 0.00M;
    public Decimal Juros { get; set; } = 0.00M;
    public Decimal ValorTotal { get; set; } = 0.00M;
    public Decimal TaxaAplicada { get; set; } = 0.0000M;
}

public class TaxaDTO
{
    public required string data { get; set; }
    public required string valor { get; set; }
}

public class SimulacaoInvestimentoService
{
    public async Task<decimal> ConsultarTaxaDiaFechado()
    {
        var dataAtual = DateTime.Now;
        var dataAtualToString = dataAtual.ToString("dd/MM/yyyy");
        var dataAnt = DateTime.Now.AddDays(-1);
        var dataAntToString = dataAnt.ToString("dd/MM/yyyy");

        var options = new RestClientOptions("http://api.bcb.gov.br");
        var client = new RestClient(options);
        var request = new RestRequest($"dados/serie/bcdata.sgs.12/dados");

        object parameters = new { formato = "json", dataInicial = dataAntToString, dataFinal = dataAtualToString };

        List<TaxaDTO> response = await client.GetJsonAsync<List<TaxaDTO>>(request.Resource, parameters, CancellationToken.None) ?? new List<TaxaDTO>();

        if (response.Count > 0)
        {
            var taxa = response.LastOrDefault();

            decimal.TryParse(taxa?.valor, out var taxaAplicada);

            return taxaAplicada;
        }
        else
        {
            return 0.0000M;
        }
    }


    public Investimento CalculaInvestimento(decimal capitalInvestido, decimal taxa, EPeriod period, int periodAmount)
    {
        switch (period)
        {
            case EPeriod.Days:
                {
                    var taxaCalc = (decimal)((taxa * periodAmount)/100);
                    var jurosPago = taxaCalc * capitalInvestido;
                    jurosPago = jurosPago - (((decimal)(calcularIRCDI(periodAmount))) * jurosPago);

                    return new Investimento() { jurosPago = jurosPago, taxaAplicada = taxaCalc };
                }
            case EPeriod.Months:
                {
                    var taxaCalc = (decimal)(((taxa * 20)/100) * periodAmount);
                    var jurosPago = taxaCalc * capitalInvestido;
                    jurosPago = jurosPago - (((decimal)(calcularIRCDI(periodAmount * 20))) * jurosPago);
                    return new Investimento() { jurosPago = jurosPago, taxaAplicada = taxaCalc };
                }
            case EPeriod.Years:
                {
                    var taxaCalc = (decimal)(((taxa*240)/100) * periodAmount);
                    var jurosPago = (taxaCalc * capitalInvestido);
                    jurosPago = jurosPago - (((decimal)(calcularIRCDI(periodAmount * 240))) * jurosPago);

                    return new Investimento() { jurosPago = jurosPago, taxaAplicada = taxaCalc };
                }
            default:
                {

                    return new Investimento() { jurosPago = 0.00M, taxaAplicada = 0.00M };
                };
        }
    }

    public double calcularIRCDI(int dias)
    {
        if(dias >=0 && dias <= 180)
        {
            return 0.225;
        }

        if (dias >= 181 && dias <= 360)
        {
            return 0.20;
        }

        if (dias >= 361 && dias <= 720)
        {
            return 0.175;
        }

        if (dias > 720)
        {
            return 0.15;
        }

        return 0.00;
    }

    public async Task<SimulacaoInvestimentoDTO> SimulacaoInvestimento(decimal capitalInvestido, EPeriod period, int periodAmount)
    {
        var taxa = await ConsultarTaxaDiaFechado();
        var investimento = CalculaInvestimento(capitalInvestido, taxa, period, periodAmount);

        return new SimulacaoInvestimentoDTO() { Investido = capitalInvestido, Juros = investimento.jurosPago, TaxaAplicada = investimento.taxaAplicada, ValorTotal = capitalInvestido + investimento.jurosPago};
    }
}


