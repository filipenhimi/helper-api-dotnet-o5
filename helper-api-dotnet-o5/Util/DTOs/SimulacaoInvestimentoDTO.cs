namespace helper_api_dotnet_o5.Util.DTOs;


public class SimulacaoInvestimentoDTO
{
    /// <summary>
    /// Valor Investido
    /// </summary>
    /// <example>5000.00</example>
    public Decimal Investido { get; set; } = 0.00M;

    /// <summary>
    /// Juros Pago
    /// </summary>
    /// <example>400.00</example>
    public Decimal Juros { get; set; } = 0.00M;

    /// <summary>
    /// Valor total (Valor Investido + Juros)
    /// </summary>
    /// <example>5400.00</example>
    public Decimal ValorTotal { get; set; } = 0.00M;

    /// <summary>
    /// Taxa Aplicada
    /// </summary>
    /// <example>10.54216</example>
    public Decimal TaxaAplicada { get; set; } = 0.0000M;
}