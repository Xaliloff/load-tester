namespace LoadTester.App.Entities.LoadTestConfiguration;

public class SimpleLoadTestConfiguration : LoadTestConfiguration
{
    public SimpleLoadTestConfiguration(string authorId) : base(authorId)
    {
    }

    public int RequestDelayInMs { get; set; } = 2;
    public string HttpUrl { get; set; } = "http://localhost:5006/WeatherForecast";
}