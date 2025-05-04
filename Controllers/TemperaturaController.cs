using GloboClima.Api.Services;
using GloboClima.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using GloboClima.Api.Interface;

namespace GloboClima.Api.Controllers
{
    [ApiController]
    [Route("temperatura")]
    public class TemperaturaController : ControllerBase
    {
        private readonly IOpenWeatherService _climaService;

        public TemperaturaController(IOpenWeatherService climaService)
        {
            _climaService = climaService;
        }

        [HttpGet("{cidadeoupais}")]
        public async Task<ActionResult<Clima>> ObterClima(string cidadeoupais)
        {
            var resultado = await _climaService.ObterClimaAsync(cidadeoupais);

            if (resultado is null)
                return NotFound("Cidade não encontrada.");

            // Extrair os dados relevantes dinamicamente
            var json = JsonDocument.Parse(JsonSerializer.Serialize(resultado));
            var main = json.RootElement.GetProperty("main");

            var clima = new Clima
            {
                Temperatura = main.GetProperty("temp").GetSingle(),
                TemperaturaMinima = main.GetProperty("temp_min").GetSingle(),
                TemperaturaMaxima = main.GetProperty("temp_max").GetSingle(),
                Umidade = main.GetProperty("humidity").GetInt32()
            };

            return Ok(clima);
        }
    }
}
