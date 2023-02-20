using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoadTester.App.Services
{
    public class LoadTestContainer
    {
        public Object LockObject = new Object();
        public List<LoadTestProcess> Tests { get; init; } = new();

        public bool AnyActiveTest()
        {
            lock (LockObject)
            {
                return Tests.Any(x => x.LoadTestProcessInfo.Status == "Running");
            }
        }
    }
}