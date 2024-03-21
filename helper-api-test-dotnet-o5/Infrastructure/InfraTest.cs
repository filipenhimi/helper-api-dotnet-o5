using helper_api_dotnet_o5.Infrastructure;
using helper_api_dotnet_o5.Models.Paises;
using Moq;

namespace helper_api_test_dotnet_o5.Infrastructure
{
    public class InfraTest
    {
        private const string ENDPOINT = "https://servicodados.ibge.gov.br/api/v1";

        [Fact]
        public async Task MetodoGET_DeveRetornarObjetoCorreto()
        {
            // Arrange
            var helperAPI = new HelperAPI(ENDPOINT);

            // Act
            var resultado = await helperAPI.MetodoGET<List<Pais>>("paises/BR");

            // Assert
            Assert.NotNull(resultado); 
            Assert.IsType<List<Pais>>(resultado);
        }

        [Fact]
        public async Task MetodoGET_DeveRetornarExcecaoQuandoEndPointInvalido()
        {
            // Arrange
            var endPointInvalido = "http://endereco.invalido";
            var helperAPI = new HelperAPI(endPointInvalido);

            // Act & Assert
            await Assert.ThrowsAsync<HttpRequestException>(async () => await helperAPI.MetodoGET<List<Pais>>("rota/teste"));
        }
    }
}
