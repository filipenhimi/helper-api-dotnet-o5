using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using helper_api_dotnet_o5.Models.Feriados;
using helper_api_dotnet_o5.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace helper_api_dotnet_o5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HolidayController : ControllerBase
    {
        private const string ENDPOINT = "https://brasilapi.com.br/api/feriados/v1/";
        private readonly ILogger<HolidayController> _logger;

        public HolidayController(ILogger<HolidayController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("{ano}")]
        [ProducesResponseType(typeof(List<RotaFeriadoOutput>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        public IActionResult Get2(string ano)
        {
            var route = $"{ano}";
            var api = new HelperAPI(ENDPOINT);
            var result = api.MetodoGET<List<RotaFeriadoOutput>>(route).Result;

            if (result.Count > 0) 
                return Ok(result);
            else
                return NoContent();
        }
    }
}