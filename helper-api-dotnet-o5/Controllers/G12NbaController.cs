using helper_api_dotnet_o5.Infrastructure;
using helper_api_dotnet_o5.Models.G12Nba;
using Microsoft.AspNetCore.Mvc;

namespace helper_api_dotnet_o5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NbaController : ControllerBase
    {
        private const string ENDPOINT = "https://api.balldontlie.io/v1";
        private const string APIKEYDEF = "5acf76f6-e117-4641-8981-64cb78b460e6";
        // @NOTE: Em um projeto real, esta Key da API **NUNCA** deveria ficar chapada no código,
        //  deveria ser apenas via variável de ambiente (EnvironmentVariable) conforme abaixo.
        // Porém para evitar causar "dificuldades" para os colegas que forem testar, ficou _hard coded_ mesmo.
        // Mas já está funcionando com env var, basta criar o arquivo .env com a chave APIKEY nba pasta src do projeto

        private string APIKEY = System.Environment.GetEnvironmentVariable("APIKEY") ?? APIKEYDEF;

        private readonly ILogger<CountryController> _logger;

        public NbaController(ILogger<CountryController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Retorna os times da NBA
        /// </summary>
        /// <remarks>
        /// Apenas times da temporada atual
        /// </remarks>
        /// <param name="conference">Filtro por conferência (opcional): East, West.</param>
        [HttpGet]
        [Route("teams")]
        [ProducesResponseType(typeof(List<Team>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Get([FromQuery] string? conference)
        {
            _logger.LogInformation($"APIKEY: {APIKEY}", DateTime.UtcNow.ToLongTimeString());
            var route = "teams";
            route += conference is not null ? $"?conference={conference}" : "";
            var api = new HelperAPI(ENDPOINT);
            try
            {
                var result = api.MetodoGET<List<Team>>(route, APIKEY, "data").Result;
                if (result.Count > 0)
                    return Ok(result);
                else
                    return NoContent();
            }
            catch (System.AggregateException err)
            {
                string msg = $"API Helper Client received a StatusError from API Server. StatusCode";
                _logger.LogInformation($"Error in MetodoGET: {err}", DateTime.UtcNow.ToLongTimeString());
                throw new HttpRequestException(msg, err.InnerException, System.Net.HttpStatusCode.Unauthorized);
            }

        }
    }
}
