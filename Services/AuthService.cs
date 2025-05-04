using GloboClima.Api.Interface;
using GloboClima.Api.Models;

namespace GloboClima.Api.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _http;
        public string Token { get; private set; }

        public AuthService(HttpClient http)
        {
            _http = http;
        }

        public async Task<bool> Login(string email, string senha)
        {
            var response = await _http.PostAsJsonAsync("login", new { email, senha });
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<TokenResponse>();
                Token = result.Token;
                _http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token);
                return true;
            }

            return false;
        }
    }
}