using helper_api_dotnet_o5.Infrastructure;
using helper_api_dotnet_o5.Models.Paises;
using Microsoft.AspNetCore.Mvc;

namespace helper_api_dotnet_o5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private const string ENDPOINT = "https://servicodados.ibge.gov.br/api/v1";
        private readonly ILogger<CountryController> _logger;

        public CountryController(ILogger<CountryController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("{sigla}")]
        [ProducesResponseType(typeof(List<Pais>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        public IActionResult Get(string sigla)
        {
            var route = $"paises/{sigla}";
            var api = new HelperAPI(ENDPOINT);
            var result = api.MetodoGET<List<Pais>>(route).Result;

            if (result.Count > 0) 
                return Ok(result);
            else
                return NoContent();
        }
    }
}
