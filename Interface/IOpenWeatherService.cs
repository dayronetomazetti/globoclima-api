namespace GloboClima.Api.Interface
{
    public interface IOpenWeatherService
    {
        Task<object> ObterClimaAsync(string cidade);
    }
}
