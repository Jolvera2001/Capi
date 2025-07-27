namespace Capi.Blazor.Models;

public class Param(string key, string value)
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Key  { get; set; } = key;
    public string Value { get; set; } = value;
}