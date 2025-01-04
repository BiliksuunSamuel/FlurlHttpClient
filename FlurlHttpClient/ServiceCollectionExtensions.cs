using Microsoft.Extensions.DependencyInjection;

namespace FlurlHttpClient;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFlurlHttpClient(this IServiceCollection services)
    {
        services.AddScoped<IHttpClientService, HttpClientService>();
        return services;
    }
}