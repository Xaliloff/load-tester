using LoadTester.App.Entities.LoadTestConfiguration;
using LoadTester.App.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LoadTester.App.Entities.WorkflowEntities;

public class SimpleWorkflow : Workflow
{
    private readonly SimpleLoadTestConfiguration _simpleConfig;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger _logger;

    public SimpleWorkflow(LoadTestConfiguration.LoadTestConfiguration loadTestConfiguration,
        IServiceScopeFactory scopeFactory,
        ILogger logger)
    {
        if (loadTestConfiguration is not SimpleLoadTestConfiguration) throw new InvalidCastException();

        _simpleConfig = (SimpleLoadTestConfiguration)loadTestConfiguration;
        ;
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    public override async Task Handle(LoadTestVirtualUser virtUser, CancellationToken ct)
    {
        using var scope = _scopeFactory.CreateScope();
        var clientFactory = scope.ServiceProvider.GetRequiredService<IHttpClientFactory>();
        var requestsRepo = scope.ServiceProvider.GetRequiredService<IRequestsRepository>();

        while (!ct.IsCancellationRequested)
        {
            await Task.Delay(1000 / _simpleConfig.RequestDelayInMs, ct);

            using var http = clientFactory.CreateClient();

            var startTime = Stopwatch.GetTimestamp();
            using var response =
                await http.GetAsync(_simpleConfig.HttpUrl, HttpCompletionOption.ResponseHeadersRead, ct);
            var elapsedTime = Stopwatch.GetElapsedTime(startTime);

            var request = new Request(virtUser.Name)
            {
                StatusCode = (int)response.StatusCode,
                ResponseTime = elapsedTime
            };
            requestsRepo.AddRequest(request);
            _logger.LogDebug("User {0} - made a request", virtUser.Name);
        }
    }
}