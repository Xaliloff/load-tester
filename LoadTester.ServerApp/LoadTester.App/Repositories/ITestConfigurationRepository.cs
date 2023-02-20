using LoadTester.App.Entities.LoadTestConfiguration;

namespace LoadTester.App.Repositories;

public interface ITestConfigurationRepository
{
    Task<List<LoadTestConfiguration>> GetAsync(Func<LoadTestConfiguration, bool> query,
        CancellationToken ct = default);

    Task<LoadTestConfiguration> GetByIdAsync(string id, CancellationToken ct);
    Task<LoadTestConfiguration> AddAsync(LoadTestConfiguration newConf, CancellationToken ct);
    Task UpdateAsync(LoadTestConfiguration conf, CancellationToken ct);
    Task RemoveAsync(string id, CancellationToken ct);
    Task<LoadTestConfiguration> AddAsync(LoadTestComplexConfiguration newConf, CancellationToken ct);
}