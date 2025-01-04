namespace FlurlHttpClient;

public class ApiResults<T>
{
    public bool IsSuccessful => Code is 200 or 201;
    public T? Data { get; set; }
    public int Code { get; set; }
    public string? Message { get; set; }
}