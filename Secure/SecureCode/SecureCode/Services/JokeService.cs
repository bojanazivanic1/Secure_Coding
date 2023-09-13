using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Security.Application;
using Newtonsoft.Json;
using SecureCode.Exceptions;
using SecureCode.Interfaces.IServices;
using SecureCode.Models;


namespace SecureCode.Services
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

            return Sanitizer.GetSafeHtmlFragment(jokeData.Setup) + "\n" + Sanitizer.GetSafeHtmlFragment(jokeData.Punchline);            
        }
    }
}
