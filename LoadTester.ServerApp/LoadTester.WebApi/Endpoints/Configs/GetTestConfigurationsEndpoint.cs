using LoadTester.App.Entities.LoadTestConfiguration;

namespace LoadTester.WebApi.Endpoints.Configs;

public class GetTestConfigurationsEndpoint : Endpoint<GetTestConfigurationsRequest, IEnumerable<LoadTestConfiguration>>
{
    public ITestConfigurationRepository TestConfigurationRepository { get; set; } = default!;
    public ICurrentUserService CurrentUserService { get; set; } = default!;

    public override void Configure()
    {
        Get("/test-configuration");
    }

    public override async Task HandleAsync(GetTestConfigurationsRequest request, CancellationToken ct)
    {
        string currentUserId = CurrentUserService.UserId ?? throw new InvalidOperationException("User not found");
        var LoadTestConfigs = await TestConfigurationRepository.GetAsync(x => x.AuthorId == currentUserId, ct);
        await SendAsync(LoadTestConfigs, StatusCodes.Status200OK, ct);
    }
}