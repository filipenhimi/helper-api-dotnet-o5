using helper_api_dotnet_o5.Infrastructure;
using helper_api_dotnet_o5.Models.Musicas;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace helper_api_dotnet_o5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusicController : ControllerBase
    {
        private const string ENDPOINT = "https://api.vagalume.com.br/search.excerpt?q=";
        private readonly ILogger<MusicController> _logger;

        public MusicController(ILogger<MusicController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{termo}")]
        [ProducesResponseType(typeof(List<Musica>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        public async Task<IActionResult> Get(string termo)
        {   
            try {
                string route = termo;
                var api = new HelperAPI(ENDPOINT);
                var result = await api.MetodoGET<MusicResponse>(route);
                
                if (result.Response.Docs.Count > 0)
                {
                    return Ok(result.Response.Docs);
                }
                
                return NoContent();
            }
            catch (Exception ex) {
                  _logger.LogError(ex, "Ocorreu um erro ao processar a requisição para termo: {termo}", termo);
                return StatusCode(500, "Ocorreu um erro interno ao processar a requisição.");
            }
           
        }     
    }
}
