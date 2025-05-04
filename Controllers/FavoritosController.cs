using GloboClima.Api.Interface;
using GloboClima.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[ApiController]
[Route("favoritos")]
public class FavoritosController : ControllerBase
{
    private readonly IFavoritoService _favoritoService;

    public FavoritosController(IFavoritoService favoritoService)
    {
        _favoritoService = favoritoService;
    }

    private string UsuarioLogado => User.FindFirstValue(ClaimTypes.Name);

    [HttpPost("cidade/{nome}")]
    public async Task<IActionResult> FavoritarCidade(string nome)
    {
        if (string.IsNullOrEmpty(UsuarioLogado))
            return Unauthorized("Usuário não autenticado.");

        var sucesso = await _favoritoService.AdicionarCidadeFavoritaAsync(UsuarioLogado, nome);

        if (!sucesso)
            return Conflict("Cidade já está nos favoritos.");

        return Ok("Cidade favoritada com sucesso.");
    }

    [HttpDelete("cidade/{nome}")]
    public async Task<IActionResult> DesfavoritarCidade(string nome)
    {
        if (string.IsNullOrEmpty(UsuarioLogado)) return Unauthorized();
        await _favoritoService.RemoverCidadeFavoritaAsync(UsuarioLogado, nome);
        return Ok("Cidade removida.");
    }

    [HttpGet("cidades")]
    public async Task<IActionResult> ListarCidades()
    {
        if (string.IsNullOrEmpty(UsuarioLogado)) return Unauthorized();
        var cidades = await _favoritoService.ListarCidadesFavoritasAsync(UsuarioLogado);
        return Ok(cidades);
    }

    [HttpPost("pais/{nome}")]
    public async Task<IActionResult> FavoritarPais(string nome)
    {
        if (string.IsNullOrEmpty(UsuarioLogado))
            return Unauthorized("Usuário não autenticado.");

        var sucesso = await _favoritoService.AdicionarPaisFavoritoAsync(UsuarioLogado, nome);

        if (!sucesso)
            return Conflict("Pais já está nos favoritos.");

        return Ok("Pais favoritado com sucesso.");
    }

    [HttpDelete("pais/{nome}")]
    public async Task<IActionResult> DesfavoritarPais(string nome)
    {
        if (string.IsNullOrEmpty(UsuarioLogado)) return Unauthorized();
        await _favoritoService.RemoverPaisFavoritoAsync(UsuarioLogado, nome);
        return Ok("País removido.");
    }

    [HttpGet("paises")]
    public async Task<IActionResult> ListarPaises()
    {
        if (string.IsNullOrEmpty(UsuarioLogado)) return Unauthorized();
        var paises = await _favoritoService.ListarPaisesFavoritosAsync(UsuarioLogado);
        return Ok(paises);
    }
}
