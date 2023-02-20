namespace LoadTester.WebApi.Endpoints.Configs;

public record RemoveTestConfigurationRequest
{
    public string Id { get; set; } = null!;
    public bool JustHide { get; set; }
}