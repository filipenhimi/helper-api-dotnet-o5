namespace helper_api_dotnet_o5.Util.DTOs;

public class SimulacaoInvestimentoDTO
{
    public Decimal Investido { get; set; } = 0.00M;
    public Decimal Juros { get; set; } = 0.00M;
    public Decimal ValorTotal { get; set; } = 0.00M;
    public Decimal TaxaAplicada { get; set; } = 0.0000M;
}