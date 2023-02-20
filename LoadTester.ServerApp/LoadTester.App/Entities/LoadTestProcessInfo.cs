namespace LoadTester.App.Entities;

public class LoadTestProcessInfo
{
    public LoadTestProcessInfo()
    {
    }

    public string Id { get; set; }
    public string Name { get; set; }
    public string Status { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndTime { get; set; }
    public List<Request> Requests { get; set; }
    public string ConfigurationId { get; set; }
}