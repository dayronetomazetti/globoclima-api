using GloboClima.Api.Services;
using Microsoft.AspNetCore.Mvc;
using GloboClima.Api.Configuration;
using System.Net.NetworkInformation;
using System.Text.Json.Serialization;
using GloboClima.Api.Interface;
namespace GloboClima.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;

    public AuthController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] LoginDto dto)
    {
        if (await _usuarioService.UsuarioExisteAsync(dto.Username))
            return Conflict("Usuário já existe.");

        await _usuarioService.RegistrarAsync(dto.Username, dto.Password);
        return Ok("Usuário registrado com sucesso.");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        try
        {
            Console.WriteLine("DTO recebido: " + dto?.Username);

            if (!await _usuarioService.ValidarLoginAsync(dto.Username, dto.Password))
                return Unauthorized("Credenciais inválidas.");

            var token = JwtConfig.GenerateToken(dto.Username);
            return Ok(new { token });
        }
        catch (Exception ex)
        {
            Console.WriteLine("ERRO LOGIN: " + ex);
            return StatusCode(500, "Erro interno no servidor.");
        }
    }

}


public class LoginDto
{
    [JsonPropertyName("username")]
    public string Username { get; set; }

    [JsonPropertyName("password")]
    public string Password { get; set; }
}
