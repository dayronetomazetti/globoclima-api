namespace GloboClima.Api.Interface
{
    public interface IUsuarioService
    {
        Task<bool> UsuarioExisteAsync(string username);
        Task RegistrarAsync(string username, string senha);
        Task<bool> ValidarLoginAsync(string username, string senha);
        string HashSenha(string senha);
    }
}
