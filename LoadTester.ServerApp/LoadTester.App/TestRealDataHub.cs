using System.Security.Claims;

namespace LoadTester.App;

public class TestRealDataHub : Hub
{
    public ITestConfigurationRepository Repo { get; }
    public TestRealDataHub(ITestConfigurationRepository repo)
    {
        this.Repo = repo;

    }
    public override async Task OnConnectedAsync()
    {
        var userId = Context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid)?.Value;
        // get users active tests
        var activeTestIds = (await Repo.GetAsync(x => x.AuthorId == userId))
            .SelectMany(x => x.Tests)
            .Where(x => x.Status == "Running")
            .Select(x => x.Id);

        foreach (var id in activeTestIds)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, id);
        }

        await base.OnConnectedAsync();
    }
}