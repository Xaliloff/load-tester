namespace LoadTester.WebApi.Endpoints.Tests;

public class StartTestEndpoint : Endpoint<StartTestRequest, string>
{
    public LoadTestService LoadTestService { get; set; } = default!;

    public override void Configure()
    {
        Post("/test-configuration/{id}/execute");
        AllowAnonymous();
        Tags("LoadTest");
    }

    public override async Task HandleAsync(StartTestRequest request, CancellationToken ct)
    {
        var testId = await LoadTestService.StartLoadTest(request.ConfigId, ct);
        await SendAsync(testId, StatusCodes.Status200OK, ct);
    }
}