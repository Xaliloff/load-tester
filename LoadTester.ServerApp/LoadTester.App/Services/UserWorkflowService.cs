using LoadTester.App.Entities.LoadTestConfiguration;
using LoadTester.App.Entities.WorkflowEntities;
using Microsoft.Extensions.DependencyInjection;

namespace LoadTester.App.Services;

public class UserWorkflowService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<UserWorkflowService> _logger;

    public UserWorkflowService(IServiceScopeFactory scopeFactory,
        ILogger<UserWorkflowService> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    public Workflow GetWorkflow(LoadTestConfiguration config,
        CancellationToken ct)
    {
        if (config is SimpleLoadTestConfiguration)
            return new SimpleWorkflow(config, _scopeFactory, _logger);

        return new ComplexWorkflow();
    }
}