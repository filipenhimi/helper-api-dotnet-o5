using helper_api_dotnet_o5.Infrastructure;
using helper_api_dotnet_o5.Models.Paises;

namespace helperapi_test_dotnet_o5.InfrastructureTest
{
    public class HelperAPITest
    {
        private const string ENDPOINT = "https://servicodados.ibge.gov.br/api/v1";

        [Fact]
        public async Task ExecutaMetodoGET_QuandoAPIERotaValida_EntaoRetornaObjetoValido()
        {
            //Arrange
            var helperAPI = new HelperAPI(ENDPOINT);
            var route = "paises/BR";

            //Act
            var result = await helperAPI.MetodoGET<List<Pais>>(route);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<Pais>>(result);
            Assert.True(result.Count() > 0);
        }
    }
}
