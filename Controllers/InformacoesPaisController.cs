using GloboClima.Api.Services;
using GloboClima.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using GloboClima.Api.Interface;

namespace GloboClima.Api.Controllers
{
    [ApiController]
    [Route("pais")]
    public class InformacoesPaisController : ControllerBase
    {
        private readonly IRestCountriesService _service;

        public InformacoesPaisController(IRestCountriesService service)
        {
            _service = service;
        }

        [HttpGet("{nome}")]
        public async Task<ActionResult<InformacoesPais>> ObterInformacoes(string nome)
        {
            var resultado = await _service.ObterPaisAsync(nome);

            if (resultado is null)
                return NotFound("País não encontrado.");

            var json = JsonDocument.Parse(JsonSerializer.Serialize(resultado));

            // A API retorna uma lista de países com aquele nome
            var pais = json.RootElement[0];

            var dto = new InformacoesPais
            {
                Nome = pais.GetProperty("name").GetProperty("common").GetString(),
                Capital = pais.TryGetProperty("capital", out var capitalArray) && capitalArray.ValueKind == JsonValueKind.Array       ? capitalArray.EnumerateArray().FirstOrDefault().GetString(): "N/A",
                Populacao = pais.GetProperty("population").GetInt64(),
                Area = pais.GetProperty("area").GetDouble(),
                Continente = pais.GetProperty("continents").EnumerateArray().FirstOrDefault().GetString(),
                Moeda = pais.GetProperty("currencies").EnumerateObject().First().Value.GetProperty("name").GetString()
            };


            return Ok(dto);
        }
    }
}
