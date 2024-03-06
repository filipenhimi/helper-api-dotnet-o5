using helper_api_dotnet_o5.Infrastructure;
using helper_api_dotnet_o5.Models.Coincap;
using Microsoft.AspNetCore.Mvc;

namespace helper_api_dotnet_o5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoincapController : ControllerBase
    {
        private const string ENDPOINT = "https://api.coincap.io/v2";
        private readonly ILogger<CoincapController> _logger;

        public CoincapController(ILogger<CoincapController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Assets>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        public IActionResult Index()
        {
            var route = $"assets";
            var api = new HelperAPI(ENDPOINT);
            var result = api.MetodoGET<List<Assets>>(route).Result;

            if (result.Count > 0) 
                return Ok(result);
            else
                return NoContent();
        }

        [HttpGet]
        [Route("{assistId}")]
        [ProducesResponseType(typeof(AssistDetail), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        public IActionResult Detail(string assistId)
        {
            var route = $"assists/{assistId}";
            var api = new HelperAPI(ENDPOINT);
            var result = api.MetodoGET<AssistDetail>(route).Result;

            if (result.Count > 0) 
                return Ok(result);
            else
                return NoContent();
        }
        
        [HttpGet]
        [Route("{assistId,interval}")]
        [ProducesResponseType(typeof(History), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        public IActionResult Detail(string assistId)
        {
            var route = $"assists/{assistId}/history?interval={interval}";
            var api = new HelperAPI(ENDPOINT);
            var result = api.MetodoGET<History>(route).Result;

            if (result.Count > 0) 
                return Ok(result);
            else
                return NoContent();
        }
    }
}
