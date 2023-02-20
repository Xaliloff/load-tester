using LoadTester.App.Entities.LoadTestConfiguration;
using LoadTester.App.Services;

namespace LoadTester.App;

public class LoadTestProcess
{
    private readonly UserCreationService _userCreator;
    private readonly IHubContext<TestRealDataHub> _hub;
    private readonly ILogger<LoadTestProcess> _logger;
    private readonly ITestRepository _testRepo;
    private readonly CancellationTokenSource _ctSource;
    private readonly CancellationToken _ct;
    private readonly List<LoadTestVirtualUser> _users = new();
    public IReadOnlyList<LoadTestVirtualUser> Users => _users.AsReadOnly();
    public IReadOnlyList<Request> Requests => Users.SelectMany(x => x.Requests).ToList().AsReadOnly();
    public LoadTestProcessInfo LoadTestProcessInfo { get; private set; }
    public TimeSpan ElapsedTime
    {
        get
        {
            if (LoadTestProcessInfo.Status == "Completed")
                return LoadTestProcessInfo.EndTime - LoadTestProcessInfo.StartDate;
            else
                return DateTime.Now - LoadTestProcessInfo.StartDate;
        }
    }

    public LoadTestProcess(UserCreationService userCreator,
        ILogger<LoadTestProcess> logger,
        ITestRepository testRepo,
        IHubContext<TestRealDataHub> hub)
    {
        _testRepo = testRepo;
        _hub = hub;
        _logger = logger;
        _userCreator = userCreator;
        _ctSource = new CancellationTokenSource();
        _ct = _ctSource.Token;

        _logger.LogInformation("LoadTestProcess created");
    }

    public async Task Start(LoadTestConfiguration config)
    {
        LoadTestProcessInfo = new LoadTestProcessInfo()
        {
            Id = Guid.NewGuid().ToString(),
            Name = $"{config.Name}-{DateTime.Now:dd/MM/yyyy HH:mm:ss}",
            ConfigurationId = config.Id,
            Status = "Running",
            StartDate = DateTime.Now,
        };
        _logger.LogInformation("Starting load test with {0} config", config.Name);

        //create initial users
        _users.AddRange(_userCreator.CreateActingUsers(config, _ct));

        //configure increase for users
        CofigureIncrease(config);

        //start initially created users
        _users.ForEach(x => x.Start(_ct));

        RunAutostop(config);

        ReportState();

        await _testRepo.Add(LoadTestProcessInfo, _ct);

        _logger.LogInformation("load test started");
    }

    public async Task Stop()
    {
        LoadTestProcessInfo.EndTime = DateTime.Now;
        LoadTestProcessInfo.Status = "Completed";

        await _testRepo.Update(LoadTestProcessInfo, _ct);

        _ctSource.Cancel();

        _logger.LogInformation("Load test stopped");
    }

    private void ReportState()
    {
        _ = Task.Run(async () =>
        {
            while (!_ct.IsCancellationRequested)
            {
                await Task.Delay(1000);
                var state = LoadTestStateViewer.GetState(this);
                //LoadTestStateViewer.View(this);
                await _hub.Clients.Group(LoadTestProcessInfo.Id).SendAsync(LoadTestProcessInfo.Id, state);
            }
        }, cancellationToken: _ct);
    }

    private void CofigureIncrease(LoadTestConfiguration config)
    {
        if (config.IncreaseUsersBy > 0)
        {
            _ = Task.Run(async () =>
            {
                while (!_ct.IsCancellationRequested)
                {
                    await Task.Delay(TimeSpan.FromSeconds(config.NewUsersEveryXSec));
                    _userCreator.CreateActingUsers(config, _ct).ToList()
                        .ForEach(u =>
                        {
                            _users.Add(u);
                            u.Start(_ct);
                        });
                    _logger.LogInformation($"Users increased to {_users.Count}");
                }

                _logger.LogInformation("New users adding stopped");
            }, cancellationToken: _ct);
        }
    }

    public void RunAutostop(LoadTestConfiguration conf)
    {
        Task.Run(async () =>
        {
            await Task.Delay(TimeSpan.FromSeconds(conf.DurationInSeconds));
            await Stop();
            LoadTestProcessInfo.EndTime = DateTime.Now;
        }, _ct);
    }
}