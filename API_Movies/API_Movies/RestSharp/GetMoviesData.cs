using RestSharp;

namespace API_Movies.RestSharp
{
    public class GetMoviesData
    {
        public async Task <string> GetMovies(string MovieName)
        {
            var options = new RestClientOptions("https://api.themoviedb.org/3/search/movie?query="+MovieName);
            var client = new RestClient(options);
            var request = new RestRequest("");
            request.AddHeader("accept", "application/json");
            request.AddHeader("Authorization", "Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiI0ZDI1YjgyMDBhZmRjMzE0NmUxMjkzYWNhOTYzMzRlNiIsInN1YiI6IjY1ZmY0NDAwNDU5YWQ2MDE2NGY3ZjY3MyIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.9pGFXrycCY39a3JBmklN3JOQqQ4mwvUdwWi03XB5AZc");
            var response = await client.GetAsync(request);

            return response.Content;
            
        }
    }
}
