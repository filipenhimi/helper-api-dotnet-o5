using Br.Com.Parallelum.Fipe.Api;
using Br.Com.Parallelum.Fipe.Client;
using Br.Com.Parallelum.Fipe.Model;
using helper_api_dotnet_o5.Fipe;
using helper_api_dotnet_o5.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Diagnostics;
using System.Reflection;

namespace helper_api_dotnet_o5.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FipeController : ControllerBase
{

    private readonly ILogger<FipeController> _logger;
    private readonly IFipeRepository _fipeRepository;



    public FipeController(ILogger<FipeController> logger, IFipeRepository fipeRepository)
    {
        _logger = logger;
        _fipeRepository = fipeRepository;
    }


    [HttpGet]
    [Route("{ano}/{modelo}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status502BadGateway)]
    public IActionResult Get(string ano, string modelo)
    {
        Configuration config = new Configuration();
        config.BasePath = "https://parallelum.com.br/fipe/api/v2";
        // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
        HttpClient httpClient = new HttpClient();
        HttpClientHandler httpClientHandler = new HttpClientHandler();
        var apiInstance = new FipeApi(httpClient, config, httpClientHandler);
        var vehicleType = VehiclesType.Cars;//  (VehiclesType)"cars";  // VehiclesType | Type of vehicle


        var modelos = _fipeRepository.GetModelos().Where(m => m.Modelo.Name.Contains(modelo));
        if (!modelos.Any())
            return NotFound();

        foreach (var model in modelos)
        {
            int.TryParse(model.Modelo.Code, out int modelCode);
            int.TryParse(model.Marca.Code, out int brandCode);
            model.Anos = apiInstance.GetYearByModelAsync(model.Tipo, brandCode, modelCode).Result;
        }

        var modelosPesquisa = modelos.Where(m => m.Anos.Exists(a => a.Name.Contains(ano)));
        if (!modelosPesquisa.Any())
            return NotFound();

        List<FipeResult> resultList = new List<FipeResult>();
        foreach (var modPesq in modelos.Select(a => new { Modelo = a, Anos = a.Anos.Where(a => a.Name.Contains(ano)) }))
        {
            int marcaCode = int.Parse(modPesq.Modelo.Marca.Code);
            int modeloCode = int.Parse(modPesq.Modelo.Modelo.Code);


            foreach (var anoPesq in modPesq.Anos)
            {
                var info = apiInstance.GetFipeInfoAsync(modPesq.Modelo.Tipo, marcaCode, modeloCode, anoPesq.Code).Result;
                resultList.Add(info);
            }            
        }

        return Ok(resultList);


        //    Configuration config = new Configuration();
        //    config.BasePath = "https://parallelum.com.br/fipe/api/v2";
        //    // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
        //    HttpClient httpClient = new HttpClient();
        //    HttpClientHandler httpClientHandler = new HttpClientHandler();
        //    var apiInstance = new FipeApi(httpClient, config, httpClientHandler);
        //    var vehicleType = VehiclesType.Cars;//  (VehiclesType)"cars";  // VehiclesType | Type of vehicle


        //    var carros = apiInstance.GetBrandsByTypeAsync(VehiclesType.Cars).Result;
        //    var caminhoes = apiInstance.GetBrandsByTypeAsync(VehiclesType.Trucks).Result;
        //    var motos = apiInstance.GetBrandsByTypeAsync(VehiclesType.Motorcycles).Result;

        //    Dictionary<VehiclesType, List<NamedCode>> brands = new();

        //    brands.Add(VehiclesType.Cars, carros);
        //    brands.Add(VehiclesType.Trucks, caminhoes);
        //    brands.Add(VehiclesType.Motorcycles, motos);

        //    List<Model> modelos = new ();
        //    foreach (var b in brands.Take(2))
        //    {
        //        foreach (var brand in b.Value.Take(2))
        //        {
        //            int.TryParse(brand.Code, out int brandCode);
        //            var models = apiInstance.GetModelsByBrandAsync(b.Key, brandCode).Result;
        //            //foreach (var model in models.Where(m => m.Name == modelo)  )
        //            foreach (var model in models.Take(5) )
        //            {
        //                int.TryParse(model.Code, out int modelCode);
        //                var anos = apiInstance.GetYearByModelAsync(b.Key, brandCode, modelCode).Result;

        //                modelos.Add(new Model(b.Key,  brand, model, anos));
        //            }
        //        }
        //    }





        //var url = "https://parallelum.com.br/fipe/api/v1";
        //var api = new HelperAPI(url);
        //var routeTipoCarro = "carros/";
        //var routeTipoMoto = "motos/";
        //var routeTipoCaminhoes = "caminhoes";
        //var routeMarcas = "marcas/{0}";
        //var routeModelos = "modelos/{0}";

        //// Buscar Tipos: "carros" para "motos" ou "caminhoes"

        //// Marcas
        //var marcasCarros = api.MetodoGET<List<Marca>>("carros/marcas").Result;
        //// var marcasMotos = api.MetodoGET<List<Marca>>("motos/marcas").Result;
        //// var marcasCaminhoes = api.MetodoGET<List<Marca>>("caminhoes/marcas").Result;

        //var todasAsMarcas = new List<Marca>();
        //todasAsMarcas.AddRange(marcasCarros);
        //// todasAsMarcas.AddRange(marcasMotos);
        //// todasAsMarcas.AddRange(marcasCaminhoes);


        ////foreach (var item in todasAsMarcas)
        ////{
        ////    // Modelos
        ////    var modelosResult = api.MetodoGET<List<ModelosResult>>($"carros/marcas/{item.Codigo}/modelos").Result;
        ////    modelosResult.ForEach(m => m.Marca = new Marca() { Codigo = item.Codigo, Nome = item.Nome });

        ////    item.Modelos.AddRange(modelos);
        ////}

        //foreach (var item in todasAsMarcas.SelectMany(x => x.Modelos).Where(m => m.Nome.Contains(modelo)))
        //{
        //    var anos = api.MetodoGET<List<Ano>>($"carros/marcas/{item.Marca.Codigo}/modelos/{item.Codigo}/anos").Result;
        //    anos.ForEach(a => a.Modelo = new Modelo() { Codigo = item.Codigo, Marca = new Marca() { Codigo = item.Marca.Codigo } });
        //}

        //var veiculosRetorno = new List<Veiculo>();
        //foreach (var item in todasAsMarcas.SelectMany(x => x.Modelos.SelectMany(a => a.Anos).Where(a => a.Nome == ano)))
        //{
        //    var veiculos = api.MetodoGET<List<Veiculo>>($"carros/marcas/{item.Modelo.Marca.Codigo}/modelos/{item.Modelo.Codigo}/anos/{item.Codigo}").Result;

        //    veiculos.ForEach(
        //        v =>
        //        {
        //            v.MarcaVeiculo = new Marca { Codigo = item.Modelo.Marca.Codigo, Nome = item.Modelo.Marca.Nome };
        //            v.ModeloVeiculo = new Modelo { Codigo = item.Modelo.Codigo, Nome = item.Modelo.Nome };
        //            v.AnoVeiculo = new Ano { Codigo = item.Codigo, Nome = item.Nome };
        //        }
        //    );
        //}

        //return Ok(veiculosRetorno);

        return Ok("TEste");
    }
}

public class ModelosResult
{
    public string Nome { get; set; }
    public string Tipo { get; set; }
    public string Marca { get; set; }
}


//[Serializable]
//public class Marca
//{
//    public string Codigo { get; set; }
//    public string Nome { get; set; }

//    public List<Modelo> Modelos { get; set; }
//}

//[Serializable]
public class Model
{
    //readonly VehiclesType _vehiclesType;
    //readonly NamedCode _marca;
    //readonly NamedCode _modelo;
    //readonly List<NamedCode> _anos;


    public VehiclesType Tipo { get; private set; }
    public NamedCode Marca { get; private set; }
    public NamedCode Modelo { get; private set; }
    public List<NamedCode> Anos { get; private set; }

    public Model(VehiclesType vehiclesType, NamedCode marca, NamedCode modelo, List<NamedCode> anos)
    {
        if (anos == null)
            anos = new List<NamedCode>();

        Tipo = vehiclesType;
        Marca = marca;
        Anos = anos;
        Modelo = modelo;
    }


}

//[Serializable]
//public class Ano
//{
//    public string Codigo { get; set; }
//    public string Nome { get; set; }
//    public Modelo Modelo { get; set; }
//}

//[Serializable]
//public class Veiculo
//{

//    [JsonIgnore]
//    public Ano AnoVeiculo { get; set; }
//    [JsonIgnore]
//    public Modelo ModeloVeiculo { get; set; }
//    [JsonIgnore]
//    public Marca MarcaVeiculo { get; set; }


//    [JsonProperty("TipoVeiculo")]
//    public long TipoVeiculo { get; set; }

//    [JsonProperty("Valor")]
//    public string Valor { get; set; }

//    [JsonProperty("Marca")]
//    public string Marca { get; set; }

//    [JsonProperty("Modelo")]
//    public string Modelo { get; set; }

//    [JsonProperty("AnoModelo")]
//    public long AnoModelo { get; set; }

//    [JsonProperty("Combustivel")]
//    public string Combustivel { get; set; }

//    [JsonProperty("CodigoFipe")]
//    public string CodigoFipe { get; set; }

//    [JsonProperty("MesReferencia")]
//    public string MesReferencia { get; set; }

//    [JsonProperty("SiglaCombustivel")]
//    public string SiglaCombustivel { get; set; }
//}


