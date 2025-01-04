// ReSharper disable All
namespace FlurlHttpClient;

public  record HttpClientRequest<T>
{
    /// <summary>
    /// Request URL
    /// </summary>
    public string? Url { get; set; }
    
    /// <summary>
    /// Request Body Data
    /// </summary>
    public T? Data { get; set; }
    
    /// <summary>
    /// Is Oauth Token or Jwt Bearer Token
    /// </summary>
    public bool IsOauthToken { get; set; } = false;
    
    /// <summary>
    /// Headers to be added to the request
    /// </summary>
    public Dictionary<string, object>? Headers { get; set; }
    
    /// <summary>
    /// Request Authorization Token
    /// </summary>
    public string? Token { get; set; }
}