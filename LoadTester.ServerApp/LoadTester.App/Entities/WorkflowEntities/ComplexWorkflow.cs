namespace LoadTester.App.Entities.WorkflowEntities;

public class ComplexWorkflow : Workflow
{
    public override Task Handle(LoadTestVirtualUser virtUser, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}