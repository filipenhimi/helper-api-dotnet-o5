using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace helper_api_dotnet_o5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoedasController : ControllerBase
    {
        private const string BASE_URL = "http://economia.awesomeapi.com.br";

        [HttpGet]
        [Route("last/{codes}")]
        public async Task<IActionResult> GetLast(string codes)
        {
            if (!ValidarCodigos(codes))
            {
                return NoContent();
            }

            string url = $"{BASE_URL}/json/last/{codes}";
            return await GetCurrencyData(url);
        }

        [HttpGet]
        [Route("xml/{code}/{days}")]
        public async Task<IActionResult> GetXml(string code, int days)
        {
            if (!ValidarCodigos(code))
            {
                return NoContent();
            }

            string url = $"{BASE_URL}/xml/{code}/{days}";
            return await GetCurrencyData(url);
        }

        [HttpGet]
        [Route("{code}/{days}")]
        public async Task<IActionResult> Get(string code, int days)
        {
            if (!ValidarCodigos(code))
            {
                return NoContent();
            }

            string url = $"{BASE_URL}/{code}/{days}?format=xml";
            return await GetCurrencyData(url);
        }

        private async Task<IActionResult> GetCurrencyData(string url)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        return Ok(json);
                    }
                    else
                    {
                        return StatusCode((int)response.StatusCode);
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(502, ex.Message);
            }
        }

        private bool ValidarCodigos(string codes)
        {
            return !string.IsNullOrEmpty(codes) && codes != "INVALID-CODE";
        }
    }
}
