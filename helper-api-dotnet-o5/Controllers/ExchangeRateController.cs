using helper_api_dotnet_o5.Infrastructure;
using helper_api_dotnet_o5.Models.ExchangeRate;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace helper_api_dotnet_o5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExchangeRateController : ControllerBase
    {
        private const string ENDPOINT = "https://economia.awesomeapi.com.br/json";
        private readonly ILogger<ExchangeRateController> _logger;

        public ExchangeRateController(ILogger<ExchangeRateController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("{moeda}/{dias}")]
        [ProducesResponseType(typeof(List<ExchangeRateOutput>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(string moeda, int dias)
        {
            try
            {
                var route = $"daily/{moeda}/{dias}";
                var api = new HelperAPI(ENDPOINT);
                var result = api.MetodoGET<List<ExchangeRateOutput>>(route).Result;

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
           
        }
    }
}
