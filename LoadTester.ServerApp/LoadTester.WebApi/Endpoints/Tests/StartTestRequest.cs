namespace LoadTester.WebApi.Endpoints.Tests;

public class StartTestRequest
{
    [BindFrom("id")]
    public string ConfigId { get; set; } = null!;
}