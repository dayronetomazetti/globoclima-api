using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using GloboClima.Api.Services;
using GloboClima.Api.Configuration;
using Amazon.Extensions.NETCore.Setup;
using Amazon;
using GloboClima.Api.Interface;

namespace GloboClima.Api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = JwtConfig.GetValidationParameters();
                });

            services.AddAuthorization();
            var awsOptions = new AWSOptions
            {
                Region = RegionEndpoint.EUNorth1 
            };
            services.AddDefaultAWSOptions(awsOptions);
            services.AddAWSService<IAmazonDynamoDB>();
            services.AddScoped<IDynamoDBContext, DynamoDBContext>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IFavoritoService, FavoritoService>();
            services.AddHttpClient<IOpenWeatherService, OpenWeatherService>();
            services.AddHttpClient<IRestCountriesService, RestCountriesService>();
            services.AddHttpClient<IAuthService, AuthService>();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddControllers();
            services.AddEndpointsApiExplorer();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
