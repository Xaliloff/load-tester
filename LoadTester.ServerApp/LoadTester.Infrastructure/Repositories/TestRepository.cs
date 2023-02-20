using LoadTester.App.Entities;
using LoadTester.App.Entities.LoadTestConfiguration;
using LoadTester.App.Repositories;
using Newtonsoft.Json;

namespace LoadTester.Infrastructure.Repositories;

public class TestRepository : ITestRepository
{
    public TestRepository()
    {
    }

    private List<LoadTestConfiguration> _testConfigs = new();

    public Task<List<LoadTestProcessInfo>> GetLoadTests(Func<LoadTestProcessInfo, bool> query = null,
        CancellationToken ct = default)
    {
        var json = File.ReadAllText("configurationsDB.json");
        _testConfigs = JsonConvert.DeserializeObject<List<LoadTestConfiguration>>(json);
        return Task.FromResult(_testConfigs.SelectMany(x => x.Tests).Where(query).ToList());
    }

    public async Task<LoadTestProcessInfo> Add(LoadTestProcessInfo loadTest, CancellationToken ct = default)
    {
        var json = File.ReadAllText("configurationsDB.json");
        _testConfigs = JsonConvert.DeserializeObject<List<LoadTestConfiguration>>(json);

        _testConfigs.First(x => x.Id == loadTest.ConfigurationId).Tests.Add(loadTest);

        json = JsonConvert.SerializeObject(_testConfigs);
        await File.WriteAllTextAsync("configurationsDB.json", json);
        return loadTest;
    }

    public async Task<LoadTestProcessInfo> Update(LoadTestProcessInfo loadTest, CancellationToken ct = default)
    {
        var json = File.ReadAllText("configurationsDB.json");
        _testConfigs = JsonConvert.DeserializeObject<List<LoadTestConfiguration>>(json);

        var testIndex = _testConfigs.First(x => x.Id == loadTest.ConfigurationId).Tests
            .FindIndex(x => x.Id == loadTest.Id);
        _testConfigs.First(x => x.Id == loadTest.ConfigurationId).Tests[testIndex] = loadTest;

        json = JsonConvert.SerializeObject(_testConfigs);
        await File.WriteAllTextAsync("configurationsDB.json", json);

        return loadTest;
    }
}