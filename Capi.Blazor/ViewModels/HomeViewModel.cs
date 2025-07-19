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
    [ObservableProperty] private int? _status;

    [RelayCommand]
    private async Task<HttpResponseMessage> SendAsync()
    {
        if (string.IsNullOrEmpty(Path)) throw new ArgumentNullException(nameof(Path));
        await BuildUriAsync();
        _client.BaseAddress = _uriBuilder.Uri;
        Method switch
        {
            MethodEnums.Get => await _client.GetAsync(_uriBuilder.Uri),
        };
    }

    private async Task BuildUriAsync()
    {
        throw new NotImplementedException();
    }
}