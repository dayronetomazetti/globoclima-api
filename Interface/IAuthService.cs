namespace GloboClima.Api.Interface
{
    public interface IAuthService
    {
        string Token { get; }
        Task<bool> Login(string email, string senha);
    }
}
