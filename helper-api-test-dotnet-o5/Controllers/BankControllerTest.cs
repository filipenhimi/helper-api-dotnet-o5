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
        public void Deve_retornar_ok_object_result_quando_api_estiver_disponivel()
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
        public void Deve_retornar_listagem_de_bancos_que_contiver_o_parametro_name_passado_no_get()
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
        public void Dev_retornar_listagem_de_bancos_que_contiver_o_parametro_name_passado_no_get()
        {
            //Arrange
            var loggerMock = new Mock<ILogger<BankController>>();
            var sut = new BankController(loggerMock.Object);

            //Act
            var result = sut.Get("*");

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
