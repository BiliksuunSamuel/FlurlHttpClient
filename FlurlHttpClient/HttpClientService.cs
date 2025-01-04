using System.Net;
using System.Reflection;
using Microsoft.AspNetCore.Http;
#pragma warning disable CS8604 // Possible null reference argument.

// ReSharper disable All

namespace FlurlHttpClient;
using Flurl.Http;

public class HttpClientService:IHttpClientService
{
    
    /// <summary>
    /// Http Get request
    /// </summary>
    /// <param name="request"></param>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TP"></typeparam>
    /// <returns></returns>
    public async Task<ApiResults<TP>> GetAsync<TP,T>(HttpClientRequest<T> request)
    {
        if (string.IsNullOrEmpty(request.Url))
        {
            await ThrowNullExceptionError(request.Url);
        }

        try
        {
           var flurlRequest = request.ToFlurlHttpRequest();
           var serverResponse = await flurlRequest.GetAsync();
            var rawResponse = await serverResponse.GetStringAsync();
            return new ApiResults<TP>
            {
                Code = serverResponse.StatusCode,
                Data = rawResponse.FromJsonString<TP>(),
                Message = serverResponse.ResponseMessage?.ReasonPhrase
            };
        }
        catch (Exception e)
        {
            return new ApiResults<TP>
            {
                Code = (int)HttpStatusCode.InternalServerError,
                Message = e.Message
            };
        }
    }

    /// <summary>
    /// Http Post request
    /// </summary>
    /// <param name="request"></param>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TP"></typeparam>
    /// <returns></returns>
    public async Task<ApiResults<TP>> PostAsync<TP,T>(HttpClientRequest<T> request)
    {
        if (string.IsNullOrEmpty(request.Url))
        {
            await ThrowNullExceptionError(request.Url);
        }

        try
        {
            var flurlRequest = request.ToFlurlHttpRequest();
            var serverResponse = await flurlRequest.PostJsonAsync(request.Data);
            var rawResponse = await serverResponse.GetStringAsync();
            return new ApiResults<TP>
            {
                Code = serverResponse.StatusCode,
                Data = rawResponse.FromJsonString<TP>(),
                Message = serverResponse.ResponseMessage?.ReasonPhrase
            };
        }
        catch (Exception e)
        {
            return new ApiResults<TP>
            {
                Code = (int)HttpStatusCode.InternalServerError,
                Message = e.Message
            };
        }
    }

    
    /// <summary>
    /// Http Patch request
    /// </summary>
    /// <param name="request"></param>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TP"></typeparam>
    /// <returns></returns>
    public async Task<ApiResults<TP>> PatchAsync<TP,T>(HttpClientRequest<T> request)
    {
        if (string.IsNullOrEmpty(request.Url))
        {
            await ThrowNullExceptionError(request.Url);
        }

        try
        {
            var flurlRequest = request.ToFlurlHttpRequest();
            var serverResponse = await flurlRequest.PatchJsonAsync(request.Data);
            var rawResponse = await serverResponse.GetStringAsync();
            return new ApiResults<TP>
            {
                Code = serverResponse.StatusCode,
                Data = rawResponse.FromJsonString<TP>(),
                Message = serverResponse.ResponseMessage?.ReasonPhrase
            };
        }
        catch (Exception e)
        {
            return new ApiResults<TP>
            {
                Code = (int)HttpStatusCode.InternalServerError,
                Message = e.Message
            };
        }
    }

    
    /// <summary>
    /// Http Put request
    /// </summary>
    /// <param name="request"></param>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TP"></typeparam>
    /// <returns></returns>
    public async Task<ApiResults<TP>> PutAsync<TP,T>(HttpClientRequest<T> request)
    {
        if (string.IsNullOrEmpty(request.Url))
        {
            await ThrowNullExceptionError(request.Url);
        }

        try
        {
            var flurlRequest = request.ToFlurlHttpRequest();
            var serverResponse = await flurlRequest.PutJsonAsync(request.Data);
            var rawResponse = await serverResponse.GetStringAsync();
            return new ApiResults<TP>
            {
                Code = serverResponse.StatusCode,
                Data = rawResponse.FromJsonString<TP>(),
                Message = serverResponse.ResponseMessage?.ReasonPhrase
            };
        }
        catch (Exception e)
        {
            return new ApiResults<TP>
            {
                Code = (int)HttpStatusCode.InternalServerError,
                Message = e.Message
            };
        }
    }
    
    /// <summary>
    /// Http Delete request
    /// </summary>
    /// <param name="request"></param>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TP"></typeparam>
    /// <returns></returns>
    public async Task<ApiResults<TP>> DeleteAsync<TP,T>(HttpClientRequest<T> request)
    {
        if (string.IsNullOrEmpty(request.Url))
        {
            await ThrowNullExceptionError(request.Url);
        }

        try
        {
            var flurlRequest = request.ToFlurlHttpRequest();
            var serverResponse = await flurlRequest.DeleteAsync();
            var rawResponse = await serverResponse.GetStringAsync();
            return new ApiResults<TP>
            {
                Code = serverResponse.StatusCode,
                Data = rawResponse.FromJsonString<TP>(),
                Message = serverResponse.ResponseMessage?.ReasonPhrase
            };
        }
        catch (Exception e)
        {
            return new ApiResults<TP>
            {
                Code = (int)HttpStatusCode.InternalServerError,
                Message = e.Message
            };
        }
    }

    
    /// <summary>
    /// Http Multipart Form Data request
    /// </summary>
    /// <param name="request"></param>
    /// <typeparam name="TP"></typeparam>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public async Task<ApiResults<TP>> PostFormDataAsync<TP, T>(HttpClientRequest<T> request)
    {
        if (string.IsNullOrEmpty(request.Url))
        {
            await ThrowNullExceptionError(request.Url);
        }

        try
        {
            var flurlRequest = request.ToFlurlHttpRequest();
            var response = await flurlRequest.PostMultipartAsync(mp =>
            {
                // Dynamically get properties of the type T
                var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (var property in properties)
                {
                    var value = property.GetValue(request.Data);

                    if (property.PropertyType == typeof(IFormFile))
                    {
                        var file = value as IFormFile;
                        if (file != null)
                        {
                            mp.AddFile(property.Name, file.OpenReadStream(), file.FileName);
                        }
                    }
                    else if (value != null)
                    {
                        // Convert other property values to string and add to multipart form
                        mp.AddString(property.Name, value.ToString());
                    }
                }
            });

            var rawResponse = await response.GetStringAsync();
            return new ApiResults<TP>
            {
                Code = response.StatusCode,
                Data = rawResponse.FromJsonString<TP>(),
                Message = response.ResponseMessage?.ReasonPhrase
            };
        }
        catch (Exception ex)
        {
            return new ApiResults<TP>
            {
                Code = (int)HttpStatusCode.InternalServerError,
                Message = ex.Message
            };
        }
    }
    
    private static async Task<ApiResults<object>> ThrowNullExceptionError(string parameter)
    {
        await Task.CompletedTask;
        return new ApiResults<object>
        {
            Code = (int)HttpStatusCode.BadRequest,
            Message = $"{parameter} is required!"
        };
    }
}
