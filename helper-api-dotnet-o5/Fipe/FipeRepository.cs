using Br.Com.Parallelum.Fipe.Api;
using Br.Com.Parallelum.Fipe.Client;
using Br.Com.Parallelum.Fipe.Model; 
using Microsoft.Extensions.Caching.Memory; 

namespace helper_api_dotnet_o5.Fipe;

public interface IFipeRepository
{
    List<Model> GetModelos();
}

public class FipeRepository: IFipeRepository
{
    private readonly ILogger<FipeRepository> _logger;
    private readonly IMemoryCache _cache;

    public FipeRepository(ILogger<FipeRepository> logger, IMemoryCache cache)
    {
        _logger = logger;
        _cache = cache;
    }

    public List<Model> Init()
    {
        Configuration config = new Configuration();
        config.BasePath = "https://parallelum.com.br/fipe/api/v2";
        // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
        HttpClient httpClient = new HttpClient();
        HttpClientHandler httpClientHandler = new HttpClientHandler();
        var apiInstance = new FipeApi(httpClient, config, httpClientHandler);
        var vehicleType = VehiclesType.Cars;//  (VehiclesType)"cars";  // VehiclesType | Type of vehicle


        var carros = apiInstance.GetBrandsByTypeAsync(VehiclesType.Cars).Result;
        var caminhoes = apiInstance.GetBrandsByTypeAsync(VehiclesType.Trucks).Result;
        var motos = apiInstance.GetBrandsByTypeAsync(VehiclesType.Motorcycles).Result;

        Dictionary<VehiclesType, List<NamedCode>> brands = new();

        brands.Add(VehiclesType.Cars, carros);
        brands.Add(VehiclesType.Trucks, caminhoes);
        brands.Add(VehiclesType.Motorcycles, motos);

        List<Model> modelos = new();
        foreach (var b in brands)
        {
            foreach (var brand in b.Value)
            {
                int.TryParse(brand.Code, out int brandCode);
                var models = apiInstance.GetModelsByBrandAsync(b.Key, brandCode).Result;

                foreach (var m in models)
                {
                    modelos.Add(new Model(b.Key, brand, m));
                }
            }
        }

        return modelos;
    }

    public List<Model> GetModelos()
    {
        var modelo = _cache.GetOrCreate("modelos", entry =>
         {
             entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60);
             return Init();
         });

        return modelo;
    }
}


public class Model
{

    public VehiclesType Tipo { get; private set; }
    public NamedCode Marca { get; private set; }
    public NamedCode Modelo { get; private set; }

    public List<NamedCode> Anos { get; set; }

    public Model(VehiclesType vehiclesType, NamedCode marca, NamedCode modelo)
    {

        Tipo = vehiclesType;
        Marca = marca;
        Modelo = modelo;
    }


}
