namespace Capi.Blazor.Services;

public class ClientService
{
    private HttpClient _client =  new HttpClient();
    
    public HttpClient GetClient() => _client;
}