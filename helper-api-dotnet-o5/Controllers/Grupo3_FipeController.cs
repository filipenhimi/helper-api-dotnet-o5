using System.Runtime.Versioning;
using helper_api_dotnet_o5.Infrastructure;
using helper_api_dotnet_o5.Models.Paises;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace helper_api_dotnet_o5.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FipeController : ControllerBase
{

    private readonly ILogger<FipeController> _logger;



    public FipeController(ILogger<FipeController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [Route("{modelo}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status502BadGateway)]
    public IActionResult Get(string modelo)
    {
        var url = "https://parallelum.com.br/fipe/api/v1/";
        var api = new HelperAPI(url);
        var routeTipoCarro = "carros/";
        var routeTipoMoto = "motos/";
        var routeTipoCaminhoes = "caminhoes";
        var routeMarcas = "marcas/{0}";
        var routeModelos = "modelos/{0}";

        // Buscar Tipos: "carros" para "motos" ou "caminhoes"

        // Marcas
        var marcasCarros = api.MetodoGET<List<Marca>>("carros/marcas").Result;
        var marcasMotos = api.MetodoGET<List<Marca>>("motos/marcas").Result;
        var marcasCaminhoes = api.MetodoGET<List<Marca>>("caminhoes/marcas").Result;

        var todasAsMarcas = new List<Marca>();
        todasAsMarcas.AddRange(marcasCarros);
        todasAsMarcas.AddRange(marcasMotos);
        todasAsMarcas.AddRange(marcasCaminhoes);


        foreach (var item in todasAsMarcas)
        {
            // Modelos
            var modelos = api.MetodoGET<List<Modelo>>($"carros/marcas/{item.Codigo}/modelos").Result;
            modelos.ForEach(m => m.Marca = new Marca() { Codigo = item.Codigo, Nome = item.Nome });

            item.Modelos.AddRange(modelos);
        }

        foreach (var item in todasAsMarcas.SelectMany(x => x.Modelos))
        {
            var anos = api.MetodoGET<List<Ano>>($"carros/marcas/{item.Marca.Codigo}/modelos/{item.Codigo}/anos").Result;
            anos.ForEach(a => a.Modelo = new Modelo() { Codigo = item.Codigo, Marca = new Marca() { Codigo = item.Marca.Codigo } });
        }

        foreach (var item in todasAsMarcas.SelectMany(x => x.Modelos.SelectMany(a => a.Anos)))
        {
            var veiculos = api.MetodoGET<List<Veiculo>>($"carros/marcas/{item.Modelo.Marca.Codigo}/modelos/{item.Modelo.Codigo}/anos/{item.Codigo}").Result;

            veiculos.ForEach(
                v=> {
                    v.MarcaVeiculo = new Marca { Codigo = item.Modelo.Marca.Codigo, Nome = item.Modelo.Marca.Nome };
                    v.ModeloVeiculo = new Modelo { Codigo = item.Modelo.Codigo, Nome = item.Modelo.Nome };
                    v.AnoVeiculo = new Ano { Codigo = item.Codigo, Nome = item.Nome }; 
                } 
            );
        }


         

        // Anos
        // var anos = new List<Ano>(); 

        // Fipe



        var route = $" ";

        var result = api.MetodoGET<Veiculo>(route).Result;
        return Ok(result);
    }
}

public class Marca
{
    public string Codigo { get; set; }
    public string Nome { get; set; }

    public List<Modelo> Modelos { get; set; }
}

public class Modelo
{
    public string Codigo { get; set; }
    public string Nome { get; set; }

    public Marca Marca { get; set; }
    public List<Ano> Anos { get; set; }
}

public class Ano
{
    public string Codigo { get; set; }
    public string Nome { get; set; }
    public Modelo Modelo { get; set; }
}

public class Veiculo
{

    [JsonIgnore]
    public Ano AnoVeiculo { get; set; }
    [JsonIgnore]
    public Modelo ModeloVeiculo { get; set; }
    [JsonIgnore]
    public Marca MarcaVeiculo { get; set; }


    [JsonProperty("TipoVeiculo")]
    public long TipoVeiculo { get; set; }

    [JsonProperty("Valor")]
    public string Valor { get; set; }

    [JsonProperty("Marca")]
    public string Marca { get; set; }

    [JsonProperty("Modelo")]
    public string Modelo { get; set; }

    [JsonProperty("AnoModelo")]
    public long AnoModelo { get; set; }

    [JsonProperty("Combustivel")]
    public string Combustivel { get; set; }

    [JsonProperty("CodigoFipe")]
    public string CodigoFipe { get; set; }

    [JsonProperty("MesReferencia")]
    public string MesReferencia { get; set; }

    [JsonProperty("SiglaCombustivel")]
    public string SiglaCombustivel { get; set; }
}


