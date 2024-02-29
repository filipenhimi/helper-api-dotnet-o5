using helper_api_dotnet_o5.Util.Class;
using helper_api_dotnet_o5.Util.DTOs;
using helper_api_dotnet_o5.Util.Enums;

namespace helper_api_dotnet_o5.Util.Interfaces;

public interface ISimulacaoInvestimentoService
{
    Task<decimal> ConsultarTaxaDiaFechado();
    Investimento CalculaInvestimento(decimal capitalInvestido, decimal taxa, EPeriod period, int periodAmount);
    double calcularIRCDI(int dias);
    Task<SimulacaoInvestimentoDTO> SimulacaoInvestimento(decimal capitalInvestido, EPeriod period, int periodAmount);
}
