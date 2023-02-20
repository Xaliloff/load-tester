namespace LoadTester.App.Services.ServiceRequests;

public class LoadTestConfigCreationRequest
{
    public string Name { get; set; }
    public string Url { get; set; }
    public string Method { get; set; }
    public List<(string key, string value)> Headers { get; set; } = new();
    public string Body { get; set; }
    public int ConcurrentUsers { get; set; } = 5;
    public int NewUsersCreationIntervalInSec { get; set; } = 10;
    public int IncreaseUsersBy { get; set; } = 5;
    public int DurationInSeconds { get; set; } = 120;
    public int RequestDelayInMs { get; set; } = 500;
    public bool IsComplex { get; set; } = false;
}