using LoadTester.App.Entities.LoadTestConfiguration;

namespace LoadTester.WebApi.Endpoints.Configs;

public class GetTestConfigurationByIdEndpoint : Endpoint<GetTestConfigurationByIdRequest, LoadTestConfiguration>
{
    public ITestConfigurationRepository TestConfigurationRepository { get; set; } = default!;

    public override void Configure()
    {
        Get("/test-configuration/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetTestConfigurationByIdRequest request, CancellationToken ct)
    {
        var conf = await TestConfigurationRepository.GetByIdAsync(request.Id, ct);
        await SendAsync(conf, StatusCodes.Status200OK, ct);
    }
}