using helper_api_dotnet_o5.Infrastructure;
using helper_api_dotnet_o5.Models.Coincap;
using helper_api_dotnet_o5.Models.Paises;
using Microsoft.AspNetCore.Mvc;

namespace helper_api_dotnet_o5.Controllers
{
    public class CoincapController : ControllerBase
    {
        private const string ENDPOINT = "https://api.coincap.io/v2";
        private readonly ILogger<CoincapController> _logger;

        public CoincapController(ILogger<CoincapController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("assets")]
        [ProducesResponseType(typeof(List<Assets>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        public IActionResult Index()
        {
            var route = $"assets";
            var api = new HelperAPI(ENDPOINT);
            var result = api.MetodoGET<Assets>(route).Result;

            if (result.data.Count > 0)
                return Ok(result);
            else
                return NoContent();
        }

        [HttpGet]
        [Route("assets/{assetId}")]
        [ProducesResponseType(typeof(AssetDetail), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        public IActionResult Detail(string assetId)
        {
            var route = $"assets/{assetId}";
            var api = new HelperAPI(ENDPOINT);
            var result = api.MetodoGET<AssetDetail>(route).Result;

            if (result.data.id != null)
                return Ok(result);
            else
                return NoContent();
        }

        [HttpGet]
        [Route("assets/{assetId}/interval/{interval}")]
        [ProducesResponseType(typeof(History), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        public IActionResult History(string assetId, string interval)
        {
            var route = $"assets/{assetId}/history?interval={interval}";
            var api = new HelperAPI(ENDPOINT);
            var result = api.MetodoGET<History>(route).Result;

            if (result.data.Count > 0)
                return Ok(result);
            else
                return NoContent();
        }
    }
}
