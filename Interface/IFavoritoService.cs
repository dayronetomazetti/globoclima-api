using GloboClima.Api.Models;

namespace GloboClima.Api.Interface
{
    public interface IFavoritoService
    {
        Task<bool> AdicionarCidadeFavoritaAsync(string username, string nomeCidade);
        Task RemoverCidadeFavoritaAsync(string username, string nomeCidade);
        Task<List<CidadeFavorita>> ListarCidadesFavoritasAsync(string username);

        Task<bool> AdicionarPaisFavoritoAsync(string username, string nomePais);
        Task RemoverPaisFavoritoAsync(string username, string nomePais);
        Task<List<PaisFavorito>> ListarPaisesFavoritosAsync(string username);
    }
}
