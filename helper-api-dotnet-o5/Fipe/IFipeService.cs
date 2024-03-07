using Br.Com.Parallelum.Fipe.Model;

namespace helper_api_dotnet_o5.Fipe;

public interface IFipeService
{
    void InitCache();

    List<ModelFipe> GetModelos(VehiclesType type);

    List<NamedCode> GetAnos(ModelFipe model);

    List<InfoFipe> GetFipeResult(VehiclesType type, string ano, string modelo);
}
