namespace FlurlHttpClient;

public interface IHttpClientService
{
    Task<ApiResults<TP>> GetAsync<TP, T>(HttpClientRequest<T> request);
    Task<ApiResults<TP>> PostAsync<TP, T>(HttpClientRequest<T> request);
    Task<ApiResults<TP>> PatchAsync<TP, T>(HttpClientRequest<T> request);
    Task<ApiResults<TP>> PutAsync<TP, T>(HttpClientRequest<T> request);
    Task<ApiResults<TP>> DeleteAsync<TP, T>(HttpClientRequest<T> request);
    Task<ApiResults<TP>> PostFormDataAsync<TP, T>(HttpClientRequest<T> request);
}