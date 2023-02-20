using LoadTester.App;

namespace LoadTester.App;

public static class LoadTestStateViewer
{
    public static LoadTestState GetState(LoadTestProcess test)
    {
        var requests = test.Requests.ToList();
        var users = test.Users.ToList();
        var elapsedTime = test.ElapsedTime;

        var reqsLastSec = requests.Where(r => r.EndTime > DateTime.Now.AddSeconds(-1)).ToList();
        var reqsLastMin = requests.Where(r => r.EndTime > DateTime.Now.AddMinutes(-1)).ToList();
        var reqsLastHour = requests.Where(r => r.EndTime > DateTime.Now.AddHours(-1)).ToList();

        return new LoadTestState()
        {
            UserCount = users.Count,
            TestRunningTime = elapsedTime.ToString("hh\\:mm\\:ss"),
            RequestCount = requests.Count,
            RequestCountPerSecond = reqsLastSec.Count(),
            RequestCountPerMinute = reqsLastMin.Count(),
            RequestCountPerHour = reqsLastHour.Count(),
            ResponseCount200 = requests.Count(r => r.StatusCode == 200),
            ResponseCount200PerSecond = reqsLastSec.Count(r => r.StatusCode == 200),
            ResponseCount200PerMinute = reqsLastMin.Count(r => r.StatusCode == 200),
            ResponseCount200PerHour = reqsLastHour.Count(r => r.StatusCode == 200),
            // AverageResponseTimeTotal = requests.Average(r => r.ResponseTime),
            // AverageResponseTimeLastSecond = reqsLastSec.Any() ? reqsLastSec.Average(r => r.ResponseTime) : 0,
            // AverageResponseTimeLastMinute = reqsLastMin.Any() ? reqsLastMin.Average(r => r.ResponseTime) : 0,
            // AverageResponseTimeLastHour = reqsLastHour.Any() ? reqsLastHour.Average(r => r.ResponseTime) : 0,
            // HighestResponseTimeTotal = requests.Max(r => r.ResponseTime),
            // LowestResponseTimeTotal = requests.Min(r => r.ResponseTime)
        };
    }
}