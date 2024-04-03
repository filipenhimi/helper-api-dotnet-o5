using API_Movies.Models.Movies;
using API_Movies.RestSharp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace API_Movies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly ILogger<MoviesController> _logger;

        public MoviesController(ILogger<MoviesController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Result>), StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> Get(string movieName)
        {
      
            var api = new GetMoviesData();
            var result = api.GetMovies(movieName).Result;
            var retorno  = JsonConvert.DeserializeObject<Welcome>(result);
            return Ok(retorno.Results);
        }
        
    }
}
