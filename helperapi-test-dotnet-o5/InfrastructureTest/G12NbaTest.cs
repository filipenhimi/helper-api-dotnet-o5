using System.Runtime.InteropServices;
using helper_api_dotnet_o5.Infrastructure;
using helper_api_dotnet_o5.Models.G12Nba;
using Xunit.Abstractions;


namespace helperapi_test_dotnet_o5.InfrastructureTest
{
    public class G12NbaTest
    {
        private readonly ITestOutputHelper _output;
        private const string APIKEY = "5acf76f6-e117-4641-8981-64cb78b460e6";
        private const string ENDPOINT = "https://api.balldontlie.io/v1";

        public G12NbaTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public async Task ExecutaGET_ApiNba_RotaValida_RetornaObjetoValido()
        {
            // Arrange
            var helperAPI = new HelperAPI(ENDPOINT);
            var route = "teams";

            // Logs
            _output.WriteLine($"SO:        {RuntimeInformation.OSDescription}");
            _output.WriteLine($"APIKEY:    {APIKEY}");
            _output.WriteLine($"ENDPOINT:  {ENDPOINT} -- {DateTime.UtcNow.ToLongTimeString()}");


            // Act
            var result = await helperAPI.MetodoGET<List<Team>>(route, APIKEY, "data");

            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<Team>>(result);
            Assert.True(result.Count() > 0);
        }

        [Fact]
        public async Task ExecutaGET_ApiNba_RotaValida_SemRetornarResultados()
        {
            // Arrange
            var helperAPI = new HelperAPI(ENDPOINT);
            var route = "teams";
            var conference = "Weast"; //Invalid COnference, Correct is West
            route += conference is not null ? $"?conference={conference}" : "";

            // Act
            var result = await helperAPI.MetodoGET<List<Team>>(route, APIKEY, "data");

            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<Team>>(result);
            Assert.True(result.Count() == 0);
        }

    }
}
