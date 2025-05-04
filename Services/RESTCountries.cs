using GloboClima.Api.Interface;
using System.Net.Http.Json;

namespace GloboClima.Api.Services
{
    public class RestCountriesService : IRestCountriesService
    {
        private readonly HttpClient _http;

        public RestCountriesService(HttpClient http) => _http = http;

        public async Task<object> ObterPaisAsync(string nome)
        {
            var url = $"https://restcountries.com/v3.1/translation/{nome}";
            return await _http.GetFromJsonAsync<object>(url);
        }
    }
}