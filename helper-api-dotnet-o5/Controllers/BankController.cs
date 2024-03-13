using helper_api_dotnet_o5.Infrastructure;
using helper_api_dotnet_o5.Models.Bancos;
using Microsoft.AspNetCore.Mvc;

namespace helper_api_dotnet_o5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {
        private const string ENDPOINT = "https://brasilapi.com.br/api/banks/v1";
        private readonly ILogger<BankController> _logger;

        public BankController(ILogger<BankController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("name/{name}")]
        [ProducesResponseType(typeof(List<Banco>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        public IActionResult Get(string name)
        {
            try
            {
                _logger.LogInformation($"Buscando todos os bancos cadastrados");

                var api = new HelperAPI(ENDPOINT);
                var result = api.MetodoGET<List<Banco>>(string.Empty).Result;

                if (result.Count > 0)
                {

                    _logger.LogInformation($"Filtrando bancos que contenham: {name}");

                    var bancos = ObterListaDeBancosComNomesSimilares(result, name);

                    return Ok(bancos);
                }
                else
                    return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao buscar bancos cadastrados.");

                throw;
            }
            
        }

        private List<Banco> ObterListaDeBancosComNomesSimilares(List<Banco> result, string name)
            => result.FindAll(banco => banco != null && banco.Codigo != null && banco.NomeCompleto.ToUpper().Contains(name.ToUpper()));
    }
}
