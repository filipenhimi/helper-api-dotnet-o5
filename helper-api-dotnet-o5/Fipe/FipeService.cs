using Br.Com.Parallelum.Fipe.Api;
using Br.Com.Parallelum.Fipe.Client;
using Br.Com.Parallelum.Fipe.Model;
using Microsoft.Extensions.Caching.Memory;

namespace helper_api_dotnet_o5.Fipe;

public class FipeService : IFipeService
{
    private readonly ILogger<FipeService> _logger;
    private readonly IMemoryCache _cache;
    private readonly FipeApi _apiInstance;

    public FipeService(ILogger<FipeService> logger, IMemoryCache cache)
    {
        _logger = logger;
        _cache = cache;

        Configuration config = new();
        config.BasePath = "https://parallelum.com.br/fipe/api/v2";
        HttpClient httpClient = new HttpClient();
        HttpClientHandler httpClientHandler = new HttpClientHandler();
        _apiInstance = new FipeApi(httpClient, config, httpClientHandler);
    }

    public void InitCache()
    {
        GetModelos(VehiclesType.Cars);
        GetModelos(VehiclesType.Motorcycles);
        GetModelos(VehiclesType.Trucks);
    }

    public List<ModelFipe> GetModelos(VehiclesType type)
    {
        var key = $"modelos_{type}";
        var modelos = _cache.GetOrCreate(key, entry =>
         {
             entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60);
             return GetModelosByType(type);
         });

        return modelos ?? Enumerable.Empty<ModelFipe>().ToList();
    }

    public List<NamedCode> GetAnos(ModelFipe model)
    {
        try
        {
            _logger.LogInformation($"Buscando anos do modelo {model.Modelo.Name} - {model.Marca.Name} - {model.Tipo}");

            var key = $"anos_{model.Tipo}_{model.Modelo.GetId()}_{model.Marca.GetId()}";
            var anos = _cache.GetOrCreate(key, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60);
                return _apiInstance.GetYearByModelAsync(model.Tipo, model.Marca.GetId(), model.Modelo.GetId()).Result;

            });

            return anos ?? Enumerable.Empty<NamedCode>().ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erro ao tentar buscar anos do modelo {model.Modelo.Name} - {model.Marca.Name} - {model.Tipo}");
            throw;
        } 
    }

    public List<InfoFipe> GetFipeResult(VehiclesType type, string ano, string modelo)
    {
        try
        {
            var key = $"fipeResult_{type}_{ano}_{modelo}";
            var fipeResult = _cache.GetOrCreate(key, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60);
                return GetFipeResultList(type, ano, modelo);
            });
            return fipeResult ?? Enumerable.Empty<InfoFipe>().ToList();
        }
        catch (Exception ex )
        {
            _logger.LogError(ex, $"Erro ao buscar informações da fipe {type} - {ano} - {modelo}");
            throw;
        }    
    }

    private List<InfoFipe> GetFipeResultList(VehiclesType type, string ano, string modelo)
    {
        try
        { 
            var modelos = this.GetModelos(type).Where(m => m !=  null && m.Modelo != null && m.Modelo.Name.Contains(modelo, StringComparison.InvariantCultureIgnoreCase)).ToList();
            if (!modelos.Any())
                return Enumerable.Empty<InfoFipe>().ToList();

            Parallel.ForEach(modelos, new ParallelOptions() { MaxDegreeOfParallelism = 2 }, (model, state )=>
            {
                try
                {
                    Thread.Sleep(100); /// Simulando um delay para evitar bloqueio da API

                    model.Anos = this.GetAnos(model);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error ao buscar informações dos anos do modelo {model.Tipo} - {model.Marca.Name} - {model.Modelo.Name}");
                    state.Break();
                } 
            });

            var modelosPesquisa = modelos.Where(m => m.Anos.Exists(a => a.Name.Contains(ano, StringComparison.InvariantCultureIgnoreCase)));
            if (!modelosPesquisa.Any())
                return Enumerable.Empty<InfoFipe>().ToList();

            var modelosComAnos = modelos.Select(a => new
            {
                Modelo = a,
                Anos = a.Anos.Where(a => a.Name.Contains(ano, StringComparison.InvariantCultureIgnoreCase)).ToList(),
            }).ToList();

            var retorno  = new List<InfoFipe>();
            Parallel.ForEach(modelosComAnos, new ParallelOptions() { MaxDegreeOfParallelism = 2 }, (modPesq, state) =>
            {
                try
                {
                    Thread.Sleep(50);
                    foreach (var anoPesq in modPesq.Anos)
                    {
                        var info = GetFipeResult(modPesq.Modelo, anoPesq.Code);
                        if (info != null)
                        {
                            retorno.Add(info.ToInfoFipe());  
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error ao buscar informações da fipe {type} - {ano} - {modelo}");
                    state.Break();
                    throw;
                }
            });

            return retorno;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error ao buscar lista de informações da fipe {type} - {ano} - {modelo}");
            throw;
        }

        return Enumerable.Empty<InfoFipe>().ToList();
    }

    private FipeResult GetFipeResult(ModelFipe modelo, string ano)
    {
        try
        {
            _logger.LogInformation($"Buscando informações do modelo {modelo.Modelo.Name} - {modelo.Marca.Name} - {ano}");

            var key = $"fipeResult_{modelo.Tipo}_{modelo.Marca.GetId()}_{modelo.Modelo.GetId()}_{ano}";
            var fipeResult = _cache.GetOrCreate(key, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60);
                return _apiInstance.GetFipeInfoAsync(modelo.Tipo, modelo.Marca.GetId(), modelo.Modelo.GetId(), ano).Result;
            });
            return fipeResult;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao Buscar informação da Fipe");
        }
        return null;
    }

    private List<ModelFipe> GetModelosByType(VehiclesType type)
    {
        List<ModelFipe> modelos = new();
        try
        {

            _logger.LogInformation($"Buscando modelos do tipo {type}");
            var brandsOfType = _apiInstance.GetBrandsByTypeAsync(type).Result;

            Parallel.ForEach(brandsOfType, new ParallelOptions() { MaxDegreeOfParallelism = 2 }, (brand, state) =>
            {
                try
                {
                    _logger.LogInformation($"Buscando modelos da marca {brand.Name} - {type}");

                    Thread.Sleep(100); /// Simulando um delay para evitar bloqueio da API

                    var models = _apiInstance.GetModelsByBrandAsync(type, brand.GetId()).Result;
                    if (models != null)
                        modelos.AddRange(models.Select(m => new ModelFipe(type, brand, m)));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Buscando modelos da marca {brand.Name} - {type}");
                    state.Break();
                }
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao Buscar informação dos modelos por tipo ");
        }

        return modelos;
    }

}