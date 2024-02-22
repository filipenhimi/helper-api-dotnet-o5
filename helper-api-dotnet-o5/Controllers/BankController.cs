using helper_api_dotnet_o5.Infrastructure;
using helper_api_dotnet_o5.Models.Bancos;
using Microsoft.AspNetCore.Mvc;

namespace helper_api_dotnet_o5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BanksController : ControllerBase
    {
        private const string ENDPOINT = "https://brasilapi.com.br/api/banks/v1";
        private readonly ILogger<BanksController> _logger;

        public BanksController(ILogger<BanksController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("code/{code}")]
        [ProducesResponseType(typeof(Banco), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        public IActionResult GetByCode(int code)
        {
            var route = $"{code}";
            var api = new HelperAPI(ENDPOINT);
            var result = api.MetodoGET<Banco>(route).Result;

            if (result != null) 
                return Ok(result);
            else
                return NoContent();
        }

        [HttpGet]
        [Route("name/{name}")]
        [ProducesResponseType(typeof(List<Banco>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        public IActionResult GetByName(string name)
        {
            var api = new HelperAPI(ENDPOINT);
            var result = api.MetodoGET<List<Banco>>(string.Empty).Result;

            if (result.Count > 0) {

                var bancos = ObterListaDeBancosComNomesSimilares(result, name);

                return Ok(bancos);
            }
            else
                return NoContent();
        }

        private List<Banco> ObterListaDeBancosComNomesSimilares(List<Banco> result, string name)
            => result.FindAll(banco => banco != null && banco.Codigo != null && banco.NomeCompleto.ToUpper().Contains(name.ToUpper()));
    }
}
