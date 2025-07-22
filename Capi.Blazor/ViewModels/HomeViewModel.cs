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
    [ObservableProperty] private HttpContent? _content;
    [ObservableProperty] private string? _response;

    [RelayCommand]
    private async Task SendAsync()
    {
        if (string.IsNullOrEmpty(Path)) throw new ArgumentNullException(nameof(Path));
        try
        {
            switch (Method)
            {
                case MethodEnums.Get:
                    var rawResponse = await _client.GetAsync(Path);
                    Response = await rawResponse.Content.ReadAsStringAsync();
                    Status = rawResponse.StatusCode;
                    break;
                case MethodEnums.Post:
                    // Response = await _client.PostAsync(Path, Content);
                    break;
                case MethodEnums.Put:
                    // Response = await _client.PutAsync(Path, Content);
                    break;
                case MethodEnums.Delete:
                    // Response = await _client.DeleteAsync(Path);
                    break;
                default:
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private async Task BuildUriAsync()
    {
        throw new NotImplementedException();
    }
}