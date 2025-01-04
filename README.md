# FlurlHttpClient
A simple wrapper around Flurl.Http that allows you to easily make http requests and deserialize the response.

## Usage
Add FlurlHttpClient to your services in your `Startup.cs` or `Program.cs` file .
```csharp
services.AddFlurlHttpClient();
```

Inject `IFlurlHttpClient` into your class and use it to make http requests.
```csharp
public class MyService:IMyService
{
    private readonly IFlurlHttpClient _flurlHttpClient;

    public MyService(IFlurlHttpClient flurlHttpClient)
    {
        _flurlHttpClient = flurlHttpClient;
    }

    public async Task<MyResponse> GetMyResponse()
    {
        var request=HttpClientRequest<object>{
            Url="https://api.example.com/my-endpoint",
            Token="token",
            IsOauthToken=false,
            Data=null
        };
        
        return await _flurlHttpClient.GetAsync<MyResponse,object>(request);
    }
}

// your response model
public class MyResponse
{
    // your properties
}
```
## Available Methods
- `GetAsync<TP,T>(HttpClientRequest request)` : Make a get request
- `PostAsync<TP,T>(HttpClientRequest< request)` : Make a post request
- `PutAsync<TP,T>(HttpClientRequest<T> request)` : Make a put request
- `DeleteAsync<TP,T>(HttpClientRequest<T> request)` : Make a delete request
- `PatchAsync<TP,T>(HttpClientRequest<T> request)` : Make a patch request
- `PostFormDataAsync<PT,T>(HttpClientRequest<T> request)` : Make a multipart form data request

## HttpClientRequest
The `HttpClientRequest` class is used to pass the request. It contains the following properties:
- `Url` : The url of the request
- `Token` : The authorization token to be used for the request
- `IsOauthToken` : A boolean indicating if the token is an oauth token or jwt token
- `Data` : The data to be sent with the request body

## ApiResults<T>
The `ApiResults<T>` class is used to return the response from the http request. It contains the following properties:
- `Data` : The response data
- `Code` : The status code of the response
- `Message` : The message of the response
- `IsSuccessful` : A boolean indicating if the request was successful


## License

This project is licensed under the MIT License.

## Contributing
1. Fork the repository.
2. Create a new branch.
3. Make your changes.
4. Commit your changes.
5. Push your changes to your fork.
6. Submit a pull request.


## License
This project is licensed under the MIT License.


## Support

For any questions or issues,
please open an issue on GitHub or
contact us at <a href="mailto:developer.biliksuun@gmail.com">
developer.biliksuun@gmail.com</a>.

## Authors
- Samuel Biliksuun

