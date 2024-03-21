using helper_api_dotnet_o5.Infrastructure;
using helper_api_dotnet_o5.Models.Grupo13;
using helper_api_dotnet_o5.Models.Paises;

namespace helperapi_test_dotnet_o5.InfrastructureTest
{
    public class Grupo13HelperAPITest
    {
        private const string ENDPOINT = "https://db.ygoprodeck.com/api/v7/cardinfo.php";
        private const string ENDPOINT_RANDOM = "https://db.ygoprodeck.com/api/v7/randomcard.php";

        [Fact]
        public async Task ExecutaMetodoGET_QuandoAPIERotaValida_EntaoRetornaObjetoValido()
        {
            //Arrange
            var helperAPI = new Grupo13HelperAPI(ENDPOINT);
            var route = "&num=100&offset=0";

            //Act
            var result = await helperAPI.MetodoGET<ListCard>(route);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<ListCard>(result);
        }

        [Fact]
        public async Task ExecutaMetodoGETRandom_QuandoAPIERotaValida_EntaoRetornaObjetoValido()
        {
            //Arrange
            var helperAPI = new Grupo13HelperAPI(ENDPOINT_RANDOM);

            //Act
            var result = await helperAPI.MetodoGETRandom<Card>();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Card>(result);
        }
    }
}
