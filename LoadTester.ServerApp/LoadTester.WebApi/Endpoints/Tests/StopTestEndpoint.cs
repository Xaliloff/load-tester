using System.Runtime.InteropServices;

namespace LoadTester.WebApi.Endpoints.Tests;

public class StopTestEndpoint : Endpoint<StopTestEndPointRequest>
{
    public LoadTestService LoadTestService { get; set; } = default!;
    public override void Configure()
    {
        Post("/test/{id}/stop");
        AllowAnonymous();
        Tags("LoadTest");
    }

    public override async Task HandleAsync(StopTestEndPointRequest request, CancellationToken ct)
    {
        await LoadTestService.StopTest(request.Id, ct);
        await SendEmptyJsonObject(ct);
    }
}