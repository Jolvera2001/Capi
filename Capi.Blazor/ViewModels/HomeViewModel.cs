using System.Net;
using Capi.Blazor.Models;
using Capi.Blazor.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Capi.Blazor.ViewModels;

public partial class HomeViewModel(ClientService client) : ObservableObject
{
    private HttpClient _client = client.GetClient();
    // Initializes a new instance of the UriBuilder class with the specified scheme,
    // host, port number, path, and query string or fragment identifier.
    private UriBuilder _uriBuilder = new();

    [ObservableProperty] private string? _path;
    [ObservableProperty] private MethodEnums _method =  MethodEnums.Get;
    [ObservableProperty] private HttpStatusCode _status;

    [ObservableProperty] private HttpResponseMessage? _response;

    [RelayCommand]
    private async Task SendAsync()
    {
        if (string.IsNullOrEmpty(Path)) throw new ArgumentNullException(nameof(Path));
        await BuildUriAsync();
        switch (Method) 
        {
            case MethodEnums.Get:
                Response = await _client.GetAsync(Path);
                Status = Response.StatusCode;
                break;
            case MethodEnums.Post:
                // Response = await _client.PostAsync(Path);
                break;
            case MethodEnums.Put:
                // Response = await _client.PutAsync(Path);
                break;
            case MethodEnums.Delete:
                // Response = await _client.DeleteAsync(Path);
                break;
            default:
                break;
        }
    }

    private async Task BuildUriAsync()
    {
        throw new NotImplementedException();
    }
}