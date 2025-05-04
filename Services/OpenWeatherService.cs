// Services/OpenWeatherService.cs
using GloboClima.Api.Interface;
using Microsoft.OpenApi.Interfaces;
using System.Net.Http.Json;
namespace GloboClima.Api.Services
{
    public class OpenWeatherService : IOpenWeatherService
    {
        private readonly HttpClient _http;
        private const string API_KEY = "223f9213d598e841ec6f06b7c8efd952";

        public OpenWeatherService(HttpClient http) => _http = http;

        public async Task<object> ObterClimaAsync(string cidade)
        {
            var url = $"https://api.openweathermap.org/data/2.5/weather?q={cidade}&appid={API_KEY}&units=metric&lang=pt";
            return await _http.GetFromJsonAsync<object>(url);
        }
    }
}