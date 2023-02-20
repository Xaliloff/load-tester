namespace LoadTester.App.Entities;

public class LoadTestVirtualUser
{
    public List<Request> Requests { get; } = new();
    public required string Name { get; init; }
    public List<UserAction> UserActionsList { get; set; } = new();
    public bool InProgress { get; private set; }
    public Workflow Workflow { get; set; }

    public void Start(CancellationToken ct)
    {
        if (InProgress) throw new Exception();

        InProgress = true;
        Task.Run(async () => await Workflow.Handle(this, ct), ct)
            .ContinueWith(task =>
            {
                if (task.IsCompletedSuccessfully)
                {
                    InProgress = false;
                }
            }, ct);
    }
}