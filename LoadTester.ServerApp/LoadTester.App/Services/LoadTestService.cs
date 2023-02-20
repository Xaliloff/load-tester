namespace LoadTester.App.Services
{
    public class LoadTestService
    {
        private readonly ITestRepository _testRepo;
        private readonly LoadTestProcess _loadTestProcess;
        private readonly ITestConfigurationRepository _testConfigurationRepo;
        private readonly LoadTestContainer _loadTestContainer;

        public LoadTestService(LoadTestProcess testProcess,
            ITestRepository testRepo,
            ITestConfigurationRepository testConfigurationRepo,
            LoadTestContainer loadTestContainer)
        {
            _testRepo = testRepo;
            _loadTestProcess = testProcess;
            _testConfigurationRepo = testConfigurationRepo;
            _loadTestContainer = loadTestContainer;
        }

        public async Task<string> StartLoadTest(string configId, CancellationToken ct)
        {
            var config = await _testConfigurationRepo.GetByIdAsync(configId, ct);
            if (config == null) throw new Exception("Config not found");

            if (_loadTestContainer.AnyActiveTest()) throw new Exception();

            await _loadTestProcess.Start(config);

            _loadTestContainer.Tests.Add(_loadTestProcess);

            return _loadTestProcess.LoadTestProcessInfo.Id;
        }

        public async Task StopTest(string id, CancellationToken ct)
        {
            await _loadTestContainer.Tests.First(x => x.LoadTestProcessInfo.Id == id).Stop();
        }

        public Task<LoadTestProcess> GetLoadTest(string id, CancellationToken ct)
        {
            return Task.FromResult(_loadTestContainer.Tests.First(x => x.LoadTestProcessInfo.Id == id));
        }

        public async Task<List<LoadTestDto>> GetLoadTests(CancellationToken ct)
        {
            var completedTests = await _testRepo.GetLoadTests();
            var loadTests = _loadTestContainer.Tests.Where(t => t.LoadTestProcessInfo.Status == "Running")
                .Select(x => new LoadTestDto
                {
                    Id = x.LoadTestProcessInfo.Id,
                    Name = x.LoadTestProcessInfo.Name,
                    Status = "Running"
                }).ToList();

            loadTests.AddRange(completedTests.Select(x => new LoadTestDto
            {
                Id = x.Id,
                Name = x.Name,
                Status = "Completed"
            }));
            return loadTests;
        }
    }
}