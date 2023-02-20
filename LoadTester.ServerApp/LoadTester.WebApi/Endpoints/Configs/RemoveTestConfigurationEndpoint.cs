namespace LoadTester.WebApi.Endpoints.Configs;

public class RemoveTestConfigurationEndpoint : Endpoint<RemoveTestConfigurationRequest>
{
    public ITestConfigurationRepository TestConfigurationRepository { get; set; } = default!;

    public override void Configure()
    {
        Delete("/test-configuration/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(RemoveTestConfigurationRequest request, CancellationToken ct)
    {
        var conf = await TestConfigurationRepository.GetByIdAsync(request.Id, ct);
        if (request.JustHide)
        {
            conf.IsDeleted = true;
            await TestConfigurationRepository.UpdateAsync(conf, ct);
        }
        else
        {
            await TestConfigurationRepository.RemoveAsync(request.Id, ct);
        }

        await SendEmptyJsonObject(ct);
    }
}