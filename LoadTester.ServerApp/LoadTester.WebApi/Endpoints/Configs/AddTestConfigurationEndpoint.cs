using LoadTester.App.Entities.LoadTestConfiguration;

namespace LoadTester.WebApp.Endpoints.Configs;

public class AddTestConfigurationEndpoint : Endpoint<LoadTestConfigCreationRequest, LoadTestConfiguration>
{
    public ITestConfigurationRepository TestConfigurationRepository { get; set; } = default!;
    public ICurrentUserService CurrentUserService { get; set; } = default!;

    public override void Configure()
    {
        Post("/test-configuration");
        AllowAnonymous();
    }

    public override async Task HandleAsync(LoadTestConfigCreationRequest configCreationRequest, CancellationToken ct)
    {
        string currentUserId = CurrentUserService.UserId ?? throw new InvalidOperationException("User not found");

        LoadTestConfiguration config;
        if (configCreationRequest.IsComplex)
        {
            var newConf = new SimpleLoadTestConfiguration(currentUserId)
            {
                Id = Guid.NewGuid().ToString(),
                Name = configCreationRequest.Name,
                Url = configCreationRequest.Url,
                Method = configCreationRequest.Method,
                Headers = configCreationRequest.Headers.Select(x => new Header(x.key, x.value)).ToList(),
                Body = configCreationRequest.Body,
                ConcurrentUsers = configCreationRequest.ConcurrentUsers,
                NewUsersEveryXSec = configCreationRequest.NewUsersCreationIntervalInSec,
                IncreaseUsersBy = configCreationRequest.IncreaseUsersBy,
                DurationInSeconds = configCreationRequest.DurationInSeconds,
                RequestDelayInMs = configCreationRequest.RequestDelayInMs
            };
            config = await TestConfigurationRepository.AddAsync(newConf, ct);
        }
        else
        {
            var newConf = new ComplexLoadTestConfiguration(currentUserId)
            {
                Id = Guid.NewGuid().ToString(),
                Name = configCreationRequest.Name,
                Url = configCreationRequest.Url,
                Method = configCreationRequest.Method,
                Headers = configCreationRequest.Headers.Select(x => new Header(x.key, x.value)).ToList(),
                Body = configCreationRequest.Body,
                ConcurrentUsers = configCreationRequest.ConcurrentUsers,
                NewUsersEveryXSec = configCreationRequest.NewUsersCreationIntervalInSec,
                IncreaseUsersBy = configCreationRequest.IncreaseUsersBy,
                DurationInSeconds = configCreationRequest.DurationInSeconds,
            };
            config = await TestConfigurationRepository.AddAsync(newConf, ct);
        }


        await SendAsync(config, StatusCodes.Status200OK, ct);
    }
}