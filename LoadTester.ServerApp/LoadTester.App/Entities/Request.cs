namespace LoadTester.App.Entities;

public class Request
{
    public Request(string userName)
    {
        UserName = userName;
        StartTime = DateTime.Now;
    }

    public string UserName { get; }
    public int StatusCode { get; internal set; }
    public TimeSpan ResponseTime { get; internal set; }
    public DateTime StartTime { get; }
    public DateTime EndTime => StartTime.Add(ResponseTime);
}