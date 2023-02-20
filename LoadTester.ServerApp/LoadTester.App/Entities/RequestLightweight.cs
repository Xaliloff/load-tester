namespace LoadTester.App.Entities;

public class RequestLightweight
{
    public RequestLightweight(DateTime finishTime)
    {
        FinishTime = finishTime;
    }

    public DateTime FinishTime { get; }
}