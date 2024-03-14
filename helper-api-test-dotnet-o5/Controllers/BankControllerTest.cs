using helper_api_dotnet_o5.Controllers;
using helper_api_dotnet_o5.Models.Bancos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace helper_api_test_dotnet_o5.Controllers
{
    public class BankControllerTest
    {
        [Fact]
        public void Get_DeveRetornarOkObjectResultQuandoApiEstiverDisponivel()
        {
            //Arrange
            var loggerMock = new Mock<ILogger<BankController>>();
            var sut = new BankController(loggerMock.Object);

            //Act
            var result = sut.Get("banco");

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Get_DeveRetornarListagemDeBancosQueContemNomePesquisado()
        {
            //Arrange
            var loggerMock = new Mock<ILogger<BankController>>();
            var nomePesquisado = "BANCO";
            var sut = new BankController(loggerMock.Object);

            //Act
            var result = sut.Get(nomePesquisado) as OkObjectResult;
            var listagemBancos = result?.Value as List<Banco>;

            //Assert
            Assert.True(listagemBancos?.All(x => x.NomeCompleto.ToUpper().Contains(nomePesquisado)));
        }

        [Fact]
        public void Get_DeveInvocarDuasVezesLogDeInformacaoENenhumaVezLogDeErroQuandoApiEstiverDisponivel()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<BankController>>();
            var sut = new BankController(loggerMock.Object);

            // Act
            var result = sut.Get("banco");

            // Assert
            loggerMock.Verify(x => x.Log(LogLevel.Information,
                                         It.IsAny<EventId>(),
                                         It.IsAny<object>(),
                                         It.IsAny<Exception>(),
                                         (Func<object, Exception?, string>)It.IsAny<object>()), Times.Exactly(2));

            loggerMock.Verify(x => x.Log(LogLevel.Error,
                                         It.IsAny<EventId>(),
                                         It.IsAny<object>(),
                                         It.IsAny<Exception>(),
                                         (Func<object, Exception?, string>)It.IsAny<object>()), Times.Never);
        }
    }
}
