namespace LoadTester.WebApi.Endpoints.Tests;

public class GetTestInfoByIdEndpoint : Endpoint<GetTestInfoByIdRequest>
{
    public LoadTestService LoadTestService { get; set; } = default!;

    public override void Configure()
    {
        Get("/test/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetTestInfoByIdRequest request, CancellationToken ct)
    {
        var tests = await LoadTestService.GetLoadTest(request.Id, ct);
        await SendAsync(tests, StatusCodes.Status200OK, ct);
    }
}