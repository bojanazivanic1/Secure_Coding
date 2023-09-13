using InsecureCode.Exceptions;
using InsecureCode.Interfaces.IServices;
using InsecureCode.Models;
using Newtonsoft.Json;

namespace InsecureCode.Services
{
    public class JokeService : IJokeService
    {
        private readonly HttpClient _httpClient;

        public JokeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetRandomJokeAsync()
        {
            var apiUrl = "https://official-joke-api.appspot.com/random_joke";
            var response = await _httpClient.GetAsync(apiUrl);
            if (!response.IsSuccessStatusCode)
                throw new InternalServerErrorException($"Error while fetching joke: {response.StatusCode}");
            var content = await response.Content.ReadAsStringAsync();
            var jokeData = JsonConvert.DeserializeObject<JokeData>(content) ??
                throw new InternalServerErrorException("Error while fetching joke.");

            return jokeData!.Setup + "\n" + jokeData.Punchline;
        }
    }
}
