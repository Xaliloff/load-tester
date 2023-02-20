namespace LoadTester.App;

public class LoadTestState
{
    public int UserCount { get; internal set; }
    public string TestRunningTime { get; internal set; }
    public int RequestCount { get; internal set; }
    public int RequestCountPerSecond { get; internal set; }
    public int RequestCountPerMinute { get; internal set; }
    public int RequestCountPerHour { get; internal set; }
    public int RequestCountPerDay { get; internal set; }
    public int ResponseCount200 { get; internal set; }
    public int ResponseCount200PerSecond { get; internal set; }
    public int ResponseCount200PerMinute { get; internal set; }
    public int ResponseCount200PerHour { get; internal set; }
    public int ResponseCount200PerDay { get; internal set; }
    public decimal AverageResponseTimeTotal { get; internal set; }
    public decimal AverageResponseTimeLastSecond { get; internal set; }
    public decimal AverageResponseTimeLastMinute { get; internal set; }
    public decimal AverageResponseTimeLastHour { get; internal set; }
    public decimal AverageResponseTimeLastDay { get; internal set; }
    public decimal HighestResponseTimeTotal { get; internal set; }
    public decimal LowestResponseTimeTotal { get; internal set; }
}