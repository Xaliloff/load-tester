using System.Data;
using Dapper;
using LoadTester.App.Entities.LoadTestConfiguration;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using LoadTester.App.Entities;
using LoadTester.App.Repositories;
using Microsoft.Data.Sqlite;

namespace LoadTester.Infrastructure.Repositories;

public class TestConfigurationRepository : ITestConfigurationRepository
{
    private readonly List<LoadTestConfiguration> _configurations = new();
    public readonly ITestRepository _testingRepository;
    private string _connectionString;

    public TestConfigurationRepository(IConfiguration configuration, ITestRepository testingRepository)
    {
        _testingRepository = testingRepository ?? throw new ArgumentNullException(nameof(testingRepository));
        _connectionString = configuration.GetConnectionString("Default");

        if (File.Exists("configurationsDB.json"))
        {
            var json = File.ReadAllText("configurationsDB.json");
            _configurations = JsonConvert.DeserializeObject<List<LoadTestConfiguration>>(json) ?? new();
        }
    }

    public Task<List<LoadTestConfiguration>> GetAsync(Func<LoadTestConfiguration, bool> query,
        CancellationToken ct = default)
    {
        return Task.FromResult(_configurations.Where(query).ToList());
    }

    public async Task<LoadTestConfiguration> GetByIdAsync(string id, CancellationToken ct)
    {
        using IDbConnection db = new SqliteConnection(_connectionString);
        var loadTestConfig = await db.QueryFirstAsync<LoadTestConfiguration>(
            @"SELECT *
                 FROM LoadTestConfigs o
                 WHERE Id = @Id",
            new { Id = id });
        loadTestConfig.Tests = (await db.QueryAsync<LoadTestProcessInfo>(
            @"SELECT * FROM Tests WHERE ConfigId = @ConfigId",
            new { ConfigId = id })).ToList();

        return loadTestConfig;
    }

    public async Task<LoadTestConfiguration> AddAsync(LoadTestConfiguration newConf, CancellationToken ct)
    {
        _configurations.Add(newConf);
        var json = JsonConvert.SerializeObject(_configurations);
        await File.WriteAllTextAsync("configurationsDB.json", json);
        return newConf;
    }

    public async Task UpdateAsync(LoadTestConfiguration conf, CancellationToken ct)
    {
        var index = _configurations.FindIndex(x => x.Id == conf.Id);
        _configurations[index] = conf;

        await File.WriteAllTextAsync("configurationsDB.json", JsonConvert.SerializeObject(_configurations));
    }

    public async Task RemoveAsync(string id, CancellationToken ct)
    {
        var index = _configurations.FindIndex(x => x.Id == id);
        _configurations.RemoveAt(index);

        await File.WriteAllTextAsync("configurationsDB.json", JsonConvert.SerializeObject(_configurations));
    }

    public async Task<LoadTestConfiguration> AddAsync(LoadTestComplexConfiguration newConf, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}