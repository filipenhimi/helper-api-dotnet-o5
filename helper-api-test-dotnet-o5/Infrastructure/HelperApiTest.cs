using helper_api_dotnet_o5.Infrastructure;
using helper_api_dotnet_o5.Models.ExchangeRate;

namespace helperapi_test_dotnet.InfrastructureTest

{

    public class HelperAPITest

    {

        private const string ENDPOINT = "https://economia.awesomeapi.com.br/json";

        [Fact]

        public async Task ExecutaMetodoGET_QuandoAPIERotaValida_EntaoRetornaObjetoValido()

        {

            //Arrange

            var helperAPI = new HelperAPI(ENDPOINT);

            var route = "USD-BRL/1";


            //Act

            var result = await helperAPI.MetodoGET<List<ExchangeRateOutput>>(route);

            //Assert

            Assert.NotNull(result);

            Assert.IsType<List<ExchangeRateOutput>>(result);

            Assert.True(result.Count() > 0);

        }
    }

}
