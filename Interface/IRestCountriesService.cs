namespace GloboClima.Api.Interface
{
    public interface IRestCountriesService
    {
        Task<object> ObterPaisAsync(string nome);
    }
}
