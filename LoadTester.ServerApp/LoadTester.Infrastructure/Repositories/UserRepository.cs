using LoadTester.App.Entities;
using LoadTester.App.Repositories;
using Newtonsoft.Json;

namespace LoadTester.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly List<AppUser> _users = new();

    public UserRepository()
    {
        if (File.Exists("usersDb.json"))
        {
            var json = File.ReadAllText("usersDb.json");
            _users = JsonConvert.DeserializeObject<List<AppUser>>(json);
        }
    }

    public Task<List<AppUser>> GetAsync(CancellationToken ct)
    {
        return Task.FromResult(_users);
    }

    public Task<AppUser> GetByIdAsync(string id, CancellationToken ct)
    {
        return Task.FromResult(_users.FirstOrDefault(x => x.Id == id));
    }

    public Task<AppUser> GetByUserNameAsync(string userName, CancellationToken ct)
    {
        return Task.FromResult(_users.FirstOrDefault(x => x.UserName == userName));
    }

    public async Task<AppUser> AddAsync(AppUser user, CancellationToken ct)
    {
        _users.Add(user);
        var json = JsonConvert.SerializeObject(_users);
        await File.WriteAllTextAsync("usersDb.json", json);
        return user;
    }
}