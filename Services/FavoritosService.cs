using Amazon.DynamoDBv2.DataModel;
using GloboClima.Api.Interface;
using GloboClima.Api.Models;

namespace GloboClima.Api.Services
{
    public class FavoritoService : IFavoritoService
    {
        private readonly IDynamoDBContext _context;

        public FavoritoService(IDynamoDBContext context)
        {
            _context = context;
        }

        // Cidades
        public async Task<bool> AdicionarCidadeFavoritaAsync(string username, string nomeCidade)
        {
            var existente = await _context.LoadAsync<CidadeFavorita>(username, nomeCidade);
            if (existente != null)
                return false; 

            var favorito = new CidadeFavorita
            {
                Username = username,
                NomeCidade = nomeCidade
            };

            await _context.SaveAsync(favorito);
            return true;
        }

        public async Task RemoverCidadeFavoritaAsync(string username, string nomeCidade)
        {
            await _context.DeleteAsync<CidadeFavorita>(username, nomeCidade);
        }

        public async Task<List<CidadeFavorita>> ListarCidadesFavoritasAsync(string username)
        {
            var query = _context.QueryAsync<CidadeFavorita>(username);
            return await query.GetRemainingAsync();
        }

        // Países
        public async Task<bool> AdicionarPaisFavoritoAsync(string username, string nomePais)
        {
                var existente = await _context.LoadAsync<PaisFavorito>(username, nomePais);
                if (existente != null)
                    return false; 

                var favorito = new PaisFavorito
                {
                    Username = username,
                    NomePais = nomePais
                };

                await _context.SaveAsync(favorito);
                return true;         
        }

        public async Task RemoverPaisFavoritoAsync(string username, string nomePais)
        {
            await _context.DeleteAsync<PaisFavorito>(username, nomePais);
        }

        public async Task<List<PaisFavorito>> ListarPaisesFavoritosAsync(string username)
        {
            var query = _context.QueryAsync<PaisFavorito>(username);
            return await query.GetRemainingAsync();
        }
    }
}
