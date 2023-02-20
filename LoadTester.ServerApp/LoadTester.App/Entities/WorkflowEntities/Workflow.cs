namespace LoadTester.App.Entities.WorkflowEntities;

public abstract class Workflow
{
    public abstract Task Handle(LoadTestVirtualUser virtUser, CancellationToken ct);
}