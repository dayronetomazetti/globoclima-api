using Amazon.DynamoDBv2.DataModel;
using GloboClima.Api.Interface;
using GloboClima.Api.Models;
using System.Security.Cryptography;
using System.Text;

namespace GloboClima.Api.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IDynamoDBContext _context;

        public UsuarioService(IDynamoDBContext context)
        {
            _context = context;
        }

        public async Task<bool> UsuarioExisteAsync(string username)
        {
            var user = await _context.LoadAsync<Usuario>(username);
            return user != null;
        }

        public async Task RegistrarAsync(string username, string senha)
        {
            var hash = HashSenha(senha);
            var user = new Usuario { Username = username, PasswordHash = hash };
            await _context.SaveAsync(user);
        }

        public async Task<bool> ValidarLoginAsync(string username, string senha)
        {
            var user = await _context.LoadAsync<Usuario>(username);
            if (user == null) return false;
            return user.PasswordHash == HashSenha(senha);
        }

        public string HashSenha(string senha)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(senha);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}