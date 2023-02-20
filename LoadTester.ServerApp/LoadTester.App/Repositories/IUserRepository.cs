namespace LoadTester.App.Repositories;

public interface IUserRepository
{
    Task<List<AppUser>> GetAsync(CancellationToken ct);

    Task<AppUser> GetByIdAsync(string id, CancellationToken ct);

    Task<AppUser> GetByUserNameAsync(string userName, CancellationToken ct);

    Task<AppUser> AddAsync(AppUser user, CancellationToken ct);
}