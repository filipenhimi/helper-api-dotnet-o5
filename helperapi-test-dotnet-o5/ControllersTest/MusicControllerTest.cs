
using helper_api_dotnet_o5.Controllers;
using helper_api_dotnet_o5.Models.Musicas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace helperapi_test_dotnet_o5.ControllersTest
{
    public class MusicControllerTest
    {

        [Fact]
        public async Task ExecutaRotaMusicasGET_QuandoAPIDisponivelEParametrosOK_EntaoRetornaObjetoValido()
        {
            // 1- Arrange
            var loggerMock = new Mock<ILogger<MusicController>>();
            var controller = new MusicController(loggerMock.Object);
            var parametroHttp = "Tempo Perdido";

            // 2- Act
            var result = await controller.Get(parametroHttp);

            // 3- Assert

            // Verifica se o resultado é do tipo OkObjectResult
            // (Http Status 200 - OK)
            Assert.IsType<OkObjectResult>(result);

            // Verifica se o resultado é diferente de nulo
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);

            // Verifica se o valor do resultado é uma lista de músicas
            var musicas = okResult.Value as List<Musica>;
            Assert.NotNull(musicas);
        }

        [Fact]
        public async Task ExecutaRotaMusicasGET_QuandoAPIDisponivelEParametrosOK_EntaoRetornaNoContent()
        {
            // 1- Arrange
            var loggerMock = new Mock<ILogger<MusicController>>();
            var controller = new MusicController(loggerMock.Object);
            var parametroHttp = "";

            // 2- Act
            var result = await controller.Get(parametroHttp);

            // 3- Assert

            // Verifica se o resultado é do tipo NoContentResult 
            // (Http Status 204 - No Content)
            Assert.IsType<NoContentResult>(result);

            // Verifica se o resultado é nulo
            var noContentResult = result as NoContentResult;
            Assert.NotNull(noContentResult);
        }

        [Theory]
        [InlineData("Tempo Perdido")]
        [InlineData("")]
        public async Task ExecutaRotaMusicasGET_QuandoAPIDisponivelEParametrosOK_EntaoRetornaObjetoValidoOuInvalido(string nomeMusica)
        {
            // 1- Arrange
            var loggerMock = new Mock<ILogger<MusicController>>();
            var controller = new MusicController(loggerMock.Object);
            var parametroHttp = nomeMusica;

            // 2- Act
            var result = await controller.Get(parametroHttp);

            // 3- Assert
            if(parametroHttp == "Tempo Perdido")
            {
                // Verifica se o resultado é do tipo OkObjectResult
                // (Http Status 200 - OK)
                Assert.IsType<OkObjectResult>(result);

                // Verifica se o resultado é diferente de nulo
                var okResult = result as OkObjectResult;
                Assert.NotNull(okResult);

                // Verifica se o valor do resultado é uma lista de músicas
                var musicas = okResult.Value as List<Musica>;
                Assert.NotNull(musicas);
            }
            else
            {
                // Verifica se o resultado é do tipo NoContentResult 
                // (Http Status 204 - No Content)
                Assert.IsType<NoContentResult>(result);

                // Verifica se o resultado é nulo
                var noContentResult = result as NoContentResult;
                Assert.NotNull(noContentResult);
            }
        }
    }

}
