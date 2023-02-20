namespace LoadTester.App.Entities.WorkflowEntities;

public abstract class UserAction
{
    protected UserAction()
    {
        DelayAfterAction = 0;
    }

    protected UserAction(int delayAfterAction)
    {
        DelayAfterAction = delayAfterAction;
    }

    protected abstract Func<Task> Action { get; set; }
    protected UserAction NextAction { get; set; }

    protected async Task Handle()
    {
        await Action();
        await Task.Delay(DelayAfterAction);
        await NextAction.Handle();
    }

    private int DelayAfterAction { get; set; }
}