using helper_api_dotnet_o5.Infrastructure;
using helper_api_dotnet_o5.Models.Grupo13;
using Microsoft.AspNetCore.Mvc;

namespace helper_api_dotnet_o5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Grupo13YgoProDeckController : ControllerBase
    {
        private const string ENDPOINT = "https://db.ygoprodeck.com/api/v7/cardinfo.php";
        private const string ENDPOINT_RANDOM = "https://db.ygoprodeck.com/api/v7/randomcard.php";
        private readonly ILogger<Grupo13YgoProDeckController> _logger;

        public Grupo13YgoProDeckController(ILogger<Grupo13YgoProDeckController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ListCard), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        public IActionResult Get([FromQuery] CardRequestDto request)
        {
            var route = "";

            if (!String.IsNullOrEmpty(request.fname))
            {
                route += $"&fname={request.fname}";
            }

            if (!String.IsNullOrEmpty(request.race.ToString()))
            {
                route += $"&race={request.race.GetDisplayName()}";
            }

            if (String.IsNullOrEmpty(request.fname))
            {
                route += $"&num=100&offset=0";
            }

            var api = new Grupo13HelperAPI(ENDPOINT);
            var result = api.MetodoGET<ListCard>(route).Result;

            if (result.Data is not null ) 
                return Ok(result);
            else
                return NoContent();
        }

        [HttpGet]
        [Route("GetRandom")]
        [ProducesResponseType(typeof(Card), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        public IActionResult GetRandom()
        {
            var api = new Grupo13HelperAPI(ENDPOINT_RANDOM);
            var result = api.MetodoGETRandom<Card>().Result;

            return Ok(result);
        }
    }
}
