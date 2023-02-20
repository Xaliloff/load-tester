using LoadTester.App.Entities.LoadTestConfiguration;
using Microsoft.Extensions.DependencyInjection;

namespace LoadTester.App.Services;

public class UserCreationService
{
    private readonly ILogger<UserCreationService> _logger;
    private readonly UserWorkflowService _workflowService;
    private readonly IServiceScopeFactory _scopeFactory;

    public UserCreationService(ILogger<UserCreationService> logger,
        UserWorkflowService workflowService,
        IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
        _workflowService = workflowService;
    }

    public IEnumerable<LoadTestVirtualUser> CreateActingUsers(LoadTestConfiguration config, CancellationToken ct)
    {
        var random = new Random();
        for (int i = 0; i < config.ConcurrentUsers; i++)
        {
            var user = new LoadTestVirtualUser()
            {
                Name = random.Next(1000, 9999).ToString(),
                Workflow = _workflowService.GetWorkflow(config, ct)
            };
            yield return user;
        }

        _logger.LogInformation("{0} users created", config.ConcurrentUsers);
    }
}