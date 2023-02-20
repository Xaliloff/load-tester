namespace LoadTester.App.Entities.LoadTestConfiguration;

public class LoadTestConfiguration
{
    public LoadTestConfiguration(string authorId)
    {
        AuthorId = authorId;
    }

    public string Id { get; init; } = Guid.NewGuid().ToString();
    public string AuthorId { get; init; }
    public string Name { get; set; }
    public string Url { get; set; }
    public string Method { get; set; }
    public List<Header> Headers { get; set; }
    public string Body { get; set; }
    public int ConcurrentUsers { get; set; } = 2;
    public int NewUsersEveryXSec { get; set; } = 10;
    public int IncreaseUsersBy { get; set; } = 2;
    public int DurationInSeconds { get; set; }
    public bool IsDeleted { get; set; }
    public List<LoadTestProcessInfo> Tests { get; set; } = new();
}