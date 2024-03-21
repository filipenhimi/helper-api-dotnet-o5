using helper_api_dotnet_o5.Infrastructure;
using helper_api_dotnet_o5.Models.Personagens;
using Microsoft.AspNetCore.Mvc;


namespace helper_api_dotnet_o5.Controllers
{
    /// <summary>
    /// Controlador para realizar operações relacionadas aos personagens de Rick and Morty.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RickMortyController : ControllerBase
    {
        private const string ENDPOINT = "https://rickandmortyapi.com/api/";
        private readonly ILogger<RickMortyController> _logger;

        public RickMortyController(ILogger<RickMortyController> logger)
        {
            _logger = logger;
        }

        public HelperAPI Api { get; set; }
        public HttpClient Client { get; set; }

        /// <summary>
        /// Este método obtém informações de um personagem com base no seu ID.
        /// </summary>
        /// <param name="id">ID do personagem a ser recuperado.</param>
        /// <returns>Informações detalhadas sobre o personagem.</returns>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(RickMortyCharacter), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        public IActionResult Get(int id)
        {
            try
            {
                var route = $"character/{id}";
                var api = new HelperAPI(ENDPOINT);
                var result = api.MetodoGET<RickMortyCharacter>(route).Result;

                if (result.Id == 0 || result == null)
                    return NoContent();
                else
                    return Ok(result);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Este método busca personagens pelo nome.
        /// </summary>
        /// <param name="name">Nome do personagem a ser buscado.</param>
        /// <returns>Lista de personagens que correspondem ao nome fornecido.</returns>
        [HttpGet]
        [Route("character/{name}")]
        [ProducesResponseType(typeof(List<RickMortyCharacterList>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        public IActionResult SearchByName(string name)
        {
            try
            {
                var route = $"character/?name={name}";
                var api = new HelperAPI(ENDPOINT);
                var result = api.MetodoGET<RickMortyCharacterList>(route).Result;

                if (result.Results == null )
                    return NoContent();
                else
                    return Ok(result);

            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
