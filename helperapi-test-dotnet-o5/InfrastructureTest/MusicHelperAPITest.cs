using helper_api_dotnet_o5.Infrastructure;
using helper_api_dotnet_o5.Models.Musicas;

namespace helperapi_test_dotnet_o5.InfrastructureTest
{
    public class MusicHelperAPITest
    {
        private const string ENDPOINT = "https://api.vagalume.com.br/search.excerpt?q=";

        [Fact]
        public async Task ExecutaMetodoGET_QuandoAPIERotaValida_EntaoRetornaObjetoValido()
        {
            //Arrange
            var api = new HelperAPI(ENDPOINT);
            var route = "Tempo Perdido";

            //Act
            var result = await api.MetodoGET<MusicResponse>(route);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<MusicResponse>(result);
            Assert.True(result.Response.Docs.Count > 0);
        }
    }
}