using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime.InteropServices;

namespace helper_api_dotnet_o5.Infrastructure
{
    public class HelperAPI
    {
        private string _EndPoint;

        public HelperAPI(string EndPoint)
        {
            _EndPoint = EndPoint;
        }

        public async Task<T> MetodoGET<T>(string Route,
            [Optional] string ApiKey,
            [Optional] string JsonRootToken)
        {
            HttpClient client = new();
            // Authentication by API Key
            if (ApiKey != null)
            {
                // Este comando produz um HEADER Authorization(key) = ApiKey(value)
                client.DefaultRequestHeaders.Add("Authorization", ApiKey);
                //@TODO Depending on how the token is sent to API, you may change this command
                // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", ApiKey);
                // client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", ApiKey);
            }

            // Get Response as string
            var URI = $"{_EndPoint}/{Route}";
            var response = await client.GetAsync(URI);
            response.EnsureSuccessStatusCode();
            string responseContent = await response.Content.ReadAsStringAsync();

            // When you have an API with pagination, we need to take the correct object with the result array
            if (JsonRootToken != null)
            {
                var token = JToken.Parse(responseContent);
                if (token is JObject)
                {
                    var json = JObject.Parse(responseContent); // parse as obj
                    var data = json.SelectToken(JsonRootToken);
                    if (data != null)
                        responseContent = data.ToString();
                }
            }
            var retorno = JsonConvert.DeserializeObject<T>(responseContent);
            return retorno;
        }
    }
}
