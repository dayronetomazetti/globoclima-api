using Amazon.Lambda.AspNetCoreServer;
using GloboClima.Api;
namespace GloboClima.Api
{
    public class LambdaEntryPoint : APIGatewayProxyFunction
    {
        protected override void Init(IWebHostBuilder builder)
        {
            builder
                .UseStartup<Startup>();
        }
    }
}