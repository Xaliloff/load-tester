using LoadTester.App.Entities;
using LoadTester.App.Repositories;
using Microsoft.Extensions.Logging;

namespace LoadTester.Infrastructure.Repositories;

public class RequestsRepository : IRequestsRepository
{
    private readonly List<RequestLightweight> _requests;
    private readonly ILogger<RequestsRepository> _logger;
    private readonly Queue<Request> _requestsQueue;
    private int _totalRequestCount;
    private string _repoId;

    public RequestsRepository(ILogger<RequestsRepository> logger)
    {
        _requests = new();
        _logger = logger;
        _requestsQueue = new Queue<Request>(120000);

        _repoId = Guid.NewGuid().ToString();
        logger.LogInformation((_repoId));

        Task.Run(HandlePersistence);
    }

    private async Task HandlePersistence()
    {
        var counter = 0;
        var requests = new Request[1000];
        while (true)
        {
            if (_requestsQueue.TryDequeue(out Request request))
            {
                requests[counter] = request;
                counter++;
                if (counter < 1000) continue;

                _logger.LogInformation("reached 1000 and it is time to save!");
                await SaveToDatabase(requests);
                Array.Clear(requests);
                counter = 0;
            }
            else await Task.Delay(200);
        }
    }

    public void AddRequest(Request request)
    {
        _totalRequestCount++;
        if (_totalRequestCount % 30000 == 0)
            _logger.LogInformation("Another 30000 happened. Total requests count {0}", _totalRequestCount);
        //_requestsQueue.Enqueue(request);
        _requests.Add(new(request.EndTime));
    }

    protected async Task SaveToDatabase(Request[] requestsToSave)
    {
        await using var fileStream = new StreamWriter("");
        foreach (var request in requestsToSave)
        {
            await fileStream.WriteLineAsync(request.ToString());
        }
    }

    public IEnumerable<RequestLightweight> LastSecondRequests =>
        _requests.Where(x => x.FinishTime > DateTime.Now.AddSeconds(-1));

    public IEnumerable<RequestLightweight> LastMinuteRequests =>
        _requests.Where(x => x.FinishTime > DateTime.Now.AddMinutes(-1));

    public IEnumerable<RequestLightweight> LastHourRequests =>
        _requests.Where(x => x.FinishTime > DateTime.Now.AddHours(-1));

    public int RequestCount => _totalRequestCount;
}