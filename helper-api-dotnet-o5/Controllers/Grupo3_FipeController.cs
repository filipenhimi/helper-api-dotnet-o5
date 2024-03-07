using Br.Com.Parallelum.Fipe.Model;
using helper_api_dotnet_o5.Fipe;
using Microsoft.AspNetCore.Mvc;

namespace helper_api_dotnet_o5.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FipeController : ControllerBase
{

    private readonly ILogger<FipeController> _logger;
    private readonly IFipeService _fipeRepository;

    public FipeController(ILogger<FipeController> logger, IFipeService fipeRepository)
    {
        _logger = logger;
        _fipeRepository = fipeRepository;
    }

    [HttpGet]
    [Route("{ano}/{modelo}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status502BadGateway)]
    public ActionResult<List<InfoFipe>> Get(VehiclesType type, string ano, string modelo)
    {
        if (!Enum.IsDefined<VehiclesType>(type))
            return BadRequest();

        List<InfoFipe> resultList = _fipeRepository.GetFipeResult(type, ano, modelo);

        if (resultList.Count == 0 )
            return NoContent();

        return Ok(resultList); 
    }
} 