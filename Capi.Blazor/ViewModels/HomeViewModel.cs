using System.Collections.ObjectModel;
using System.Net;
using Capi.Blazor.Models;
using Capi.Blazor.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Capi.Blazor.ViewModels;

public partial class HomeViewModel(ClientService client) : ObservableObject
{
    private HttpClient _client = client.GetClient();
    private UriBuilder _uriBuilder = new();

    [ObservableProperty] private string? _path;
    [ObservableProperty] private MethodEnums _method =  MethodEnums.Get;
    [ObservableProperty] private HttpStatusCode _status;
    [ObservableProperty] private HttpContent? _content;
    [ObservableProperty] private string? _response;
    
    private readonly ObservableCollection<Param> _queries = new();
    public IReadOnlyCollection<Param> Queries => _queries;

    [RelayCommand]
    private async Task SendAsync()
    {
        if (string.IsNullOrEmpty(Path)) throw new ArgumentNullException(nameof(Path));
        try
        {
            HttpResponseMessage rawResponse;
            switch (Method)
            {
                case MethodEnums.Get:
                    rawResponse = await _client.GetAsync(Path);
                    await HandleResponse(rawResponse);
                    break;
                case MethodEnums.Post:
                    rawResponse = await _client.PostAsync(Path, Content);
                    await HandleResponse(rawResponse);
                    break;
                case MethodEnums.Put:
                    rawResponse = await _client.PutAsync(Path, Content);
                    await HandleResponse(rawResponse);
                    break;
                case MethodEnums.Delete:
                    rawResponse = await _client.DeleteAsync(Path);
                    await HandleResponse(rawResponse);
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
    
    public void AddParam(Param newParam)
    {
        _queries.Add(newParam);
    }

    public void RemoveParam(Param param)
    {
        _queries.Remove(param);
    }

    private async Task BuildUriAsync()
    {
        throw new NotImplementedException();
    }

    private async Task HandleResponse(HttpResponseMessage response)
    {
        Response = await response.Content.ReadAsStringAsync();
        Status = response.StatusCode;
    }

}