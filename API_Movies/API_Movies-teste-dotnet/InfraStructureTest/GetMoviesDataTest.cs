using API_Movies.RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Movies_teste_dotnet.InfraStructureTest
{
    public class GetMoviesDataTest
    {
        [Fact]
        public void ExecutaGetMovies_QuandoAPIERotaValida_EntaoRetornaObjetoValido()
        {
            //Arrange
            var getMoviesData = new GetMoviesData();
            string movieName = "Piratas do Caribe";

            //Act
            var result =  getMoviesData.GetMovies(movieName).Result;

            //Assert
            Assert.NotNull(result);
            
        }
    }
}
