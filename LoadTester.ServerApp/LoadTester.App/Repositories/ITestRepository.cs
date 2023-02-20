using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LoadTester.App.Entities.LoadTestConfiguration;

namespace LoadTester.App.Repositories;

public interface ITestRepository
{
    Task<List<LoadTestProcessInfo>> GetLoadTests(Func<LoadTestProcessInfo, bool> query = null,
        CancellationToken ct = default);

    Task<LoadTestProcessInfo> Add(LoadTestProcessInfo loadTest, CancellationToken ct = default);
    Task<LoadTestProcessInfo> Update(LoadTestProcessInfo loadTest, CancellationToken ct = default);
}