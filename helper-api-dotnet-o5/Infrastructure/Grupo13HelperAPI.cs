using Newtonsoft.Json;

namespace helper_api_dotnet_o5.Infrastructure
{
    public class Grupo13HelperAPI
    {
        private string _EndPoint;

        public Grupo13HelperAPI(string EndPoint)
        {
            _EndPoint = EndPoint;
        }

        public async Task<T> MetodoGET<T>(string Route) 
        {
            HttpClient httpClient = new();
            var URI = $"{_EndPoint}?{Route}";
            //var URI = $"{_EndPoint}";

            var response = await httpClient.GetAsync(URI);

            string responseContent = await response.Content.ReadAsStringAsync();

            var retorno = JsonConvert.DeserializeObject<T>(responseContent);

            return retorno;
        }
    }
}
