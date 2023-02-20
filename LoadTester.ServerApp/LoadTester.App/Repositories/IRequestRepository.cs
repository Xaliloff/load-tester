namespace LoadTester.App.Repositories;

public interface IRequestsRepository
{
    void AddRequest(Request request);
    IEnumerable<RequestLightweight> LastSecondRequests { get; }
    IEnumerable<RequestLightweight> LastMinuteRequests { get; }
    IEnumerable<RequestLightweight> LastHourRequests { get; }
}