namespace LoadTester.WebApi.Endpoints.Tests;

public class GetTestsListEndpoint : Endpoint<GetTestsListRequest, List<LoadTestDto>>
{
    public LoadTestService LoadTestService { get; set; } = default!;

    public override void Configure()
    {
        Get("/test");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetTestsListRequest request, CancellationToken ct)
    {
        var tests = await LoadTestService.GetLoadTests(ct);
        await SendAsync(tests, StatusCodes.Status200OK, ct);
    }
}