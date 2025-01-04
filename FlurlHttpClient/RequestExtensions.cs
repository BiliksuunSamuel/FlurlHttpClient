using Flurl.Http;

namespace FlurlHttpClient;

public static class RequestExtensions
{
    public static IFlurlRequest? ToFlurlHttpRequest<T>(this HttpClientRequest<T> request)
    {
        var url = request.Url;
        var token = request.Token;
        var isOauthToken = request.IsOauthToken;

        var flurlRequest = string.IsNullOrEmpty(token)
            ? url.AllowAnyHttpStatus()
            : isOauthToken
                ? url.WithOAuthBearerToken(token).AllowAnyHttpStatus()
                : url.AllowAnyHttpStatus()
                    .WithHeader("Authorization", token);

        // Add headers to the request
        if (request.Headers == null || request.Headers.Keys.Count < 1) return flurlRequest;
        foreach (var (key, value) in request.Headers)
        {
            flurlRequest.WithHeader(key, value);
        }

        return flurlRequest;
    }
}