using helper_api_dotnet_o5.Controllers;
using RestSharp;
using helper_api_dotnet_o5.Util.Class;
using helper_api_dotnet_o5.Util.DTOs;
using helper_api_dotnet_o5.Util.Enums;
using helper_api_dotnet_o5.Util.Interfaces;


namespace helper_api_dotnet_o5.Services;

public class SimulacaoInvestimentoService : ISimulacaoInvestimentoService
{
    private IConfiguration _configuration;

    public SimulacaoInvestimentoService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<decimal> ConsultarTaxaDiaFechado()
    {
        var dataAtual = DateTime.Now;
        var dataAtualToString = dataAtual.ToString("dd/MM/yyyy");
        var dataAnt = DateTime.Now.AddDays(-1);
        var dataAntToString = dataAnt.ToString("dd/MM/yyyy");

        var baseUrlGov = _configuration.GetSection("URL_API_GOV").Value;

        if(baseUrlGov == null)
        {
            throw new NullReferenceException("Variável de ambiente URL_API_GOV não encontrada");
        }

        var options = new RestClientOptions(baseUrlGov);
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
        var diasUtilAno = 253;
        var diasUtilMes = diasUtilAno / 12;
        var taxaAnual = taxa * diasUtilAno;
        var taxaMensal = taxaAnual / 12;
        var taxaDiaria = taxaMensal / diasUtilMes;
        Console.WriteLine($"dias UtilAno: {diasUtilAno}");
        Console.WriteLine($"dias UtilMes: {diasUtilMes}");
        Console.WriteLine($"Taxa Anual: {taxaAnual}");
        Console.WriteLine($"Taxa Mensal: {taxaMensal}");
        Console.WriteLine($"Taxa Diária: {taxaDiaria}");

        switch (period)
        {
            case EPeriod.Days:
                {
                    var taxaCalc = (decimal)((taxaDiaria) / 100);
                    var jurosPago = taxaCalc * capitalInvestido;
                    jurosPago = jurosPago - (((decimal)(calcularIRCDI(periodAmount))) * jurosPago);

                    return new Investimento() { jurosPago = jurosPago, taxaAplicada = taxaCalc };
                }
            case EPeriod.Months:
                {
                    var taxaCalc = (decimal)(((taxaMensal) / 100) * periodAmount);
                    var jurosPago = taxaCalc * capitalInvestido;
                    jurosPago = jurosPago - (((decimal)(calcularIRCDI(periodAmount * diasUtilMes))) * jurosPago);
                    return new Investimento() { jurosPago = jurosPago, taxaAplicada = taxaCalc };
                }
            case EPeriod.Years:
                {
                    var taxaCalc = (decimal)(((taxaAnual) / 100) * periodAmount);
                    var jurosPago = (taxaCalc * capitalInvestido);
                    jurosPago = jurosPago - (((decimal)(calcularIRCDI(periodAmount * diasUtilAno))) * jurosPago);

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
        if (dias >= 0 && dias <= 180)
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

        return new SimulacaoInvestimentoDTO() { Investido = capitalInvestido, Juros = investimento.jurosPago, TaxaAplicada = investimento.taxaAplicada, ValorTotal = capitalInvestido + investimento.jurosPago };
    }
}

